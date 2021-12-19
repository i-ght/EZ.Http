using System.Buffers;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using EZ.Http.curl.h;

namespace EZ.Http;


internal enum HttpResponseComponent
{
    Status,
    Headers,
    Body
}

internal readonly record struct unit()
{
    public static unit Value { get; } = new unit();
};

internal class AsyncOperation<TValue>
{
    private interface AsyncOp
    {
        readonly record struct Return(TValue Value) : AsyncOp;
        readonly record struct Throw : AsyncOp;
    }

    private readonly Channel<AsyncOp> _channel;

    public bool TryReturn(
        TValue value)
    {
        if (!_channel.Writer.TryWrite(new AsyncOp.Return(value))) {
            return false;
        }
        return _channel.Writer.TryComplete();
    }

    public void Return(
        TValue value)
    {
        if (!TryReturn(value)) {
            throw new InvalidOperationException(
                "Async operation already completed."
            );
        }
    }

    public bool TryThrow(
        Exception e)
    {
        if (!_channel.Writer.TryWrite(new AsyncOp.Throw())) {
            return false;
        }
       
        return _channel.Writer.TryComplete(e);
    }
        
    public void Throw(
        Exception e)
    {
        if (!TryThrow(e)) {
            throw new InvalidOperationException(
                "Async operation already completed."
            );
        }
    }

    public async ValueTask<TValue> Compute(
        CancellationToken cancellationToken=default)
    {
        var read =
            _channel.Reader.ReadAsync(
                cancellationToken
            ).ConfigureAwait(false);

        switch (await read) {
            case AsyncOp.Return ret:
                return ret.Value;
            case AsyncOp.Throw:
                /* will throw ------------------/\ */
                await _channel.Reader.Completion
                    .ConfigureAwait(false);
                throw null!;
            default:
                throw new InvalidOperationException();
        };
    }

    public AsyncOperation() =>
        _channel =
            Channel.CreateBounded<AsyncOp>(
                capacity: 1);
}

internal class EZHttpXfer : IDisposable
{
    private bool _disposed;
    private readonly GCHandle _gcHandle;
    private readonly Action<CURL> _freeCURL;

    /* unpwned by the class. */
    public CURL EZ { get; }
    /* owned by the class*/
    public curl_slist Headers { get; }

    public EZHttpRequest Req { get; }
    /*public Logger Logger { get; }*/
    public List<(EZHttpStatusCode, List<ReadOnlyMemory<byte>>)> ResponseHeads { get; }
    public AsyncOperation<EZHttpResponse> RcvdFinalMsgHead { get; }
    public AsyncOperation<unit> RcvdFinalRespMsg { get; }

    public Channel<ReadOnlyMemory<byte>> Body { get; }

    public List<ReadOnlyMemory<byte>> CurrentResponseHeaders {
        get {
            var (_stat, headers) =
                ResponseHeads
                .Last();
            return headers;
        }
    }

    public IntPtr Ptr =>
        /* GCHandle.ToIntPtr will give address of handle that can be round-tripped over unmanaged call.*/
        GCHandle.ToIntPtr(_gcHandle);

    public HttpResponseComponent CurrentlyReceiving { get; set; }
    public bool NeedFollowRedirect { get; set; }

    public void Dispose()
    {
        if (_disposed) {
            return;
        }
        _disposed = true;
        Body.Writer.Complete();
        Headers.Dispose();
        _gcHandle.Free();
        _freeCURL(EZ);
    }

    public EZHttpXfer(
        EZHttpRequest req,
        CURL easy,
        Action<CURL> freeCURL)
    {
        var mySelf = this;
        _gcHandle = GCHandle.Alloc(mySelf);
        _freeCURL = freeCURL;
        Req = req;
        EZ = easy; /* not owned */
        ResponseHeads = new(2);
        Body = 
            Channel.CreateBounded<ReadOnlyMemory<byte>>(
                capacity: 256
            );
        /*Logger =
            new ConsoleLogger(
                name: $"Xfer {{ CURL={(nint)easy}, Request={{ Proxy={Req.Proxy}; Uri={Req.Uri} }} }}",
                logLevel: LogLevel.Trace,
                logToStdErrThreshold: LogLevel.Warning
            );*/
        Headers = new();
        RcvdFinalMsgHead = new();
        RcvdFinalRespMsg = new();
    }
}

internal readonly struct SemaAcq : IDisposable
{
    private readonly SemaphoreSlim _lock;

    public SemaAcq(SemaphoreSlim l) => _lock = l;

    public void Release() => _lock.Release();

    public void Dispose() => Release();

    public static async Task<SemaAcq> AcquireAsync(
        SemaphoreSlim l,
        CancellationToken cancellationToken = default)
    {
        await l.WaitAsync(cancellationToken)
            .ConfigureAwait(false);
        return new SemaAcq(l);
    }

    public static SemaAcq Acquire(
        SemaphoreSlim l)
    {   
        l.Wait();
        return new SemaAcq(l);
    }
}



internal static class CurlCallbacks
{
    internal static readonly ReadOnlyMemory<byte> CrLf =
        Encoding.UTF8.GetBytes(
            "\r\n");
    internal static readonly ReadOnlyMemory<byte> SP =
        Encoding.UTF8.GetBytes(" ");

    internal static nuint ProcHeaderCallbackErr(
        EZHttpXfer xfer,
        string message)
    {
        return 0;
    }

    /*
        *(( general-header        ; Section 4.5
        | response-header        ; Section 6.2
        | entity-header ) CRLF)  ; Section 7.1
    */
    private static nuint ProcHeaderLine(
        in ReadOnlySpan<byte> line,
        EZHttpXfer xfer)
    {

        if (!line.SequenceEqual(CrLf.Span)) {
            /* Add the line of the http response message head. */
            xfer.CurrentResponseHeaders.Add(
                line.ToArray());

            return (nuint)line.Length;
        }
        /* the value of this property is set in header call back when processing status line.
            if status code is 3xx then, this property is set to true. */
        if (xfer.NeedFollowRedirect) {
            /* reset the value of this. a new request is going to be made to the redirect URI. */
            xfer.NeedFollowRedirect = false;
            /* expect to receive the status line of the next response of the redirect URI. */
            xfer.CurrentlyReceiving = HttpResponseComponent.Status;
        } else {
            var (statusCode, headers) = xfer.ResponseHeads.Last();
            var headersMap = EZHttpHeaders.OfRawData(headers);
            headers.Clear(); /* clear copies of header lines */
            var contentLength =
                CurlEz.Info<int>(
                    xfer.EZ,
                    CURLINFO.CURLINFO_CONTENT_LENGTH_DOWNLOAD_T
                );

            xfer.RcvdFinalMsgHead.Return(
                new EZHttpResponse(
                    statusCode,
                    headersMap,
                    new EZHttpResponseBody(
                        xfer.Body.Reader,
                        contentLength <= -1 ? null : contentLength
                    ),
                    xfer.Req
                )
            );

            /* the body is expected to be received next. */
            xfer.CurrentlyReceiving = HttpResponseComponent.Body;
        }

        return (nuint)line.Length;
    }

    private static nuint ProcStatusLine(
        in ReadOnlySpan<byte> line,
        EZHttpXfer xfer)
    {
        var statusCode =
            CurlEz.Info<int>(
                xfer.EZ,
                CURLINFO.CURLINFO_RESPONSE_CODE
            );
        var redirUri =
            CurlEz.Info<string>(
                xfer.EZ,
                CURLINFO.CURLINFO_REDIRECT_URL
            );

        xfer.ResponseHeads.Add(
            ((EZHttpStatusCode)statusCode,
              new List<ReadOnlyMemory<byte>>(16))
        );

        switch (statusCode, xfer.Req.FollowAutoRedirect) {
            /* redirect */
            case (var a, true) when a >= 300 && a <= 399:
                xfer.NeedFollowRedirect = true;
                goto default;
            default:
                /* headers are next */
                xfer.CurrentlyReceiving = HttpResponseComponent.Headers;
                return (nuint)line.Length;
        }
    }

    /* 
    libcurl streams the http response headers one line at a time,
    beginning with the status line and followed by the sequence of headers.
    a complete HTTP header that is passed to this function can be up to
    CURL_MAX_HTTP_HEADER (100K) bytes and includes the final line terminator.
    */
    [UnmanagedCallersOnly]
    public static unsafe nuint ProcessHeaderLine(
        nint data,
        nuint size,
        nuint nmemb,
        nint userdata)
    {
        var xfer = GCHandle.FromIntPtr(userdata).Target as EZHttpXfer;
        if (null == xfer) {
            throw new InvalidOperationException(
                "userinfo ptr data in curl process header callback is NULL."
            );
        }

        nuint length = size * nmemb;

        var line =
            new ReadOnlySpan<byte>(
                (void*)data,
                (int)length
            );

        /*
        Response      = Status-Line               ; Section 6.1
                        *(( general-header        ; Section 4.5
                        | response-header         ; Section 6.2
                        | entity-header ) CRLF)   ; Section 7.1
                        CRLF
                        [ message-body ]          ; Section 7.2
        */

        /* decide what to do */
        switch (xfer.CurrentlyReceiving) {
            /* the status line is expected to be the next line. */
            case HttpResponseComponent.Status:
                return ProcStatusLine(line, xfer);
            case HttpResponseComponent.Headers:
                return ProcHeaderLine(line, xfer);
            /* dont wan't to receive body in header callback */
            default:
                return ProcHeaderCallbackErr(
                    xfer,
                    "invalid http transfer state in curl header callback."
                );
        }
    }

    private static void WriteBodyChannel(
        EZHttpXfer xfer,
        in ReadOnlySpan<byte> span
    ) => xfer.Body.Writer.TryWrite(span.ToArray());

    [UnmanagedCallersOnly]
    public unsafe static nuint ProcRespBod(
        nint data,
        nuint size,
        nuint nmemb,
        nint userdata)
    {
        var xfer = GCHandle.FromIntPtr(userdata).Target as EZHttpXfer;
        if (null == xfer) {
            throw new InvalidOperationException(
                "userinfo ptr data in curl process header callback is NULL."
            );
        }

        var length = nmemb * size;

        var span =
            new ReadOnlySpan<byte>(
                (void*)data,
                (int)length
            );
        WriteBodyChannel(xfer, span);

        return length;
    }
}

internal static class EZHttpConduct
{
/*
    private static void Log(
        EZHttpXfer xfer,
        LogLevel logLevel,
        string fmt,
        params object?[] args
    ) => xfer.Logger.WriteEntry(logLevel, fmt, args);

    private static void Log(
        EZHttpXfer xfer,
        Exception e
    ) => xfer.Logger.WriteEntry(LogLevel.Error, "{0}", e );
*/    

    private static void SetOpt<T>(
        EZHttpXfer xfer,
        CURLoption option,
        T value)
    {
        /*Log(
            xfer,
            LogLevel.Trace,
            "[{0}] = {1}",
            option,
            value
        );*/
        CurlEz.SetOpt(xfer.EZ, option, value);
    }

    public static unsafe void ConfigureCURLoptions(
        EZHttpXfer xfer)
    {
        var (req, ez) = (xfer.Req, xfer.EZ);

        /* headers */

        foreach (var kvp in xfer.Req.Headers) {
            var (a, b) = (kvp.Key, kvp.Value);
            CurlSList.Append(xfer.Headers, $"{a}: {b}");
        }
        nint hdrsPtr = xfer.Headers;

        /* accept-encoding header */
        var acceptEncoding =
            req.Headers.ContainsKey("Accept-Encoding")
            ? req.Headers["Accept-Encoding"]
            : default;

        var hdrFunc = (nint)(delegate* unmanaged<nint, nuint, nuint, nint, nuint>)(
            &CurlCallbacks.ProcessHeaderLine
        );
        var writeFunc = (nint)(delegate* unmanaged<nint, nuint, nuint, nint, nuint>)(
            &CurlCallbacks.ProcRespBod
        );
        /*var readFunc = (nint)(delegate* unmanaged<nint, nuint, nuint, nint, nuint>)(
            &CurlCallbacks.ProcReqBody
        );*/

        var options = new List<(CURLoption, object?)> {
            /*strings*/
            (CURLoption.CURLOPT_CUSTOMREQUEST, req.Method),
            (CURLoption.CURLOPT_URL, req.Uri.AbsoluteUri),
            (CURLoption.CURLOPT_PROXY, req.Proxy?.AbsoluteUri ?? null),
            (CURLoption.CURLOPT_PROXYUSERPWD, req.Proxy?.UserInfo ?? null),
            (CURLoption.CURLOPT_ACCEPT_ENCODING, acceptEncoding),
            
            /*nints*/
            (CURLoption.CURLOPT_HEADERFUNCTION, hdrFunc) ,
            (CURLoption.CURLOPT_HEADERDATA, xfer.Ptr),
            (CURLoption.CURLOPT_WRITEFUNCTION, writeFunc),
            (CURLoption.CURLOPT_WRITEDATA, xfer.Ptr),
            (CURLoption.CURLOPT_HTTPHEADER, (nint)xfer.Headers),
            (CURLoption.CURLOPT_READDATA, (nint)0),
            (CURLoption.CURLOPT_READFUNCTION, (nint)0),
            (CURLoption.CURLOPT_PRIVATE, xfer.Ptr),

            /*bools*/
            (CURLoption.CURLOPT_VERBOSE, req.Verbose),
            (CURLoption.CURLOPT_SSL_VERIFYPEER, req.VerifyTlsCertificate),
            (CURLoption.CURLOPT_FOLLOWLOCATION, req.FollowAutoRedirect),
            (CURLoption.CURLOPT_SUPPRESS_CONNECT_HEADERS, true),

            /*longs*/
            (CURLoption.CURLOPT_TIMEOUT_MS, (long)req.Timeout.TotalMilliseconds),
        }.ToDictionary(a => a.Item1, b => b.Item2);

        foreach (var (opt, value) in options) {
            SetOpt(xfer, opt, value);
        }
    }

    private static void ConfigureCURLMoptions(
        CURLM multi,
        in EZConductorConnectionReuseCfg connectCfg)
    {
        CurlMulti.SetOpt(
            multi,
            CURLMoption.CURLMOPT_MAX_TOTAL_CONNECTIONS,
            connectCfg.MaxTotalConnections
        );
        CurlMulti.SetOpt(
            multi,
            CURLMoption.CURLMOPT_MAXCONNECTS,
            connectCfg.MaxKeepAlives
        );
        CurlMulti.SetOpt(
            multi,
            CURLMoption.CURLMOPT_MAX_HOST_CONNECTIONS,
            connectCfg.MaxKeepAlivesPerHost
        );
    }

    private static int CalcNthFibonacci(
        int n)
    {
        switch (n) {
            case 0: return 0;
            case 1: return 1;
            default: break;
        }

        var x = 0;
        var y = 1;
        var z = 0;

        for (var i = 2; i <= n; i++) {
            x = z + y;
            z = y;
            y = x;
        }

        return x;
    }

    private static void ProcCURLMsg(
        CURLM multi,
        in CURLMsg curlMsg)
    {
        if (curlMsg.@type != CURLMSG.CURLMSG_DONE) {
            throw new InvalidOperationException(
                "curl_multi_read_info returned out of range CURLMSG enum value."
            );
        }

        var ez = curlMsg.easy_handle;
        var userData = CurlEz.Info<nint>(ez, CURLINFO.CURLINFO_PRIVATE);
        var tmp = GCHandle.FromIntPtr(userData).Target as EZHttpXfer;
        if (null == tmp) {
            throw new InvalidOperationException(
                "error getting CURLINFO_PRIVATE from completed easy request."
            );
        }

        using EZHttpXfer xfer = tmp;
        CurlMulti.Remove(multi, ez);
        
        static void ProcErr(EZHttpXfer xfer, in CURLMsg curlMsg)
        {
            switch (xfer.CurrentlyReceiving) {
                case HttpResponseComponent.Status:
                case HttpResponseComponent.Headers:
                    xfer.RcvdFinalMsgHead.Throw(
                        new CurlCodeException(curlMsg.data.result)
                    );
                    return;
                case HttpResponseComponent.Body:
                    xfer.RcvdFinalRespMsg.Throw(
                        new CurlCodeException(curlMsg.data.result)
                    );
                    return;
                default:
                    throw new InvalidOperationException();

            }
        }

        switch (curlMsg.data.result) {
            case CURLcode.CURLE_OK:
                xfer.RcvdFinalRespMsg.Return(unit.Value);
                return;
            default:
                ProcErr(xfer, curlMsg);
                return;
        }
    }

    private static void ProcIO(
        SemaphoreSlim lck,
        CURLM multi,
        in CancellationToken cancellationToken,
        AsyncOperation<unit> ioLoop)
    {
        var isleep = 12; // fib 12 = 144ms
        while (!cancellationToken.IsCancellationRequested) {
            using var _ = SemaAcq.Acquire(lck);
            var zzzs = CalcNthFibonacci(n: isleep);
            var fds = CurlMulti.Poll(multi, timeoutMs: zzzs);
            if (0 == fds) {
                /* no activity on fds, isleep */
                if (isleep < 23) { // capped at fib 23 = 28657ms
                    isleep++;
                }
                continue;
            }
        
            while (CurlMulti.Perform(multi, out var _active)) {
                Thread.Sleep(3);
            }

            while (CurlMulti.ReadInfo(multi, out var msg)) {
                ProcCURLMsg(multi, msg);
            }

            /* go back to staying awake longer, there's activity to process*/
            isleep = 12; // fib 12 = 144ms
        }

        ioLoop.Return(unit.Value);
    }

    public static async Task<EZHttpResponse> RequestResponse(
        Func<CURL> acquireCURL,
        Action<CURL> freeCURL,
        EZHttpRequest req,
        CURLM multi,
        SemaphoreSlim @lock,
        CancellationToken cancellationToken)
    {
        async Task<EZHttpXfer> BeginXfer()
        {
            using var lck =
                await SemaAcq.AcquireAsync(
                    @lock, cancellationToken
                ).ConfigureAwait(false);

            CURL curl = acquireCURL();

            var xfer =
                new EZHttpXfer(
                    req,
                    curl,
                    freeCURL
                );

            try {
                ConfigureCURLoptions(xfer);
                CurlMulti.Add(multi, curl);
                var _a = CurlMulti.Perform(
                    multi, out var _b);
                return xfer;
            } catch (Exception e) when (
                   e is CurlException
                || e is InvalidOperationException
            ) {
                xfer.Dispose();
                throw;
            }
        }

        EZHttpXfer xfer = 
            await BeginXfer()
                .ConfigureAwait(false);
        EZHttpResponse resp = 
            await xfer.RcvdFinalMsgHead.Compute(
                cancellationToken
            ).ConfigureAwait(false);
        unit _ = 
            await xfer.RcvdFinalRespMsg.Compute(
                cancellationToken
            ).ConfigureAwait(false);
        return resp;
    }

    public static void BeginProcIOThread(
        in EZConductorConnectionReuseCfg connectCfg,
        CURLM multi,
        SemaphoreSlim @lock,
        CancellationToken cancellationToken,
        AsyncOperation<unit> ioLoop)
    {
        ConfigureCURLMoptions(multi, connectCfg);

        const int _256KB = 262144;
        new Thread(
            () =>
                ProcIO(
                    @lock,
                    multi,
                    cancellationToken,
                    ioLoop
                ),
            maxStackSize: _256KB
        ) {
            IsBackground = true,
        }.Start();
    }
}
