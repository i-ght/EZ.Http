using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using EZ.Http.curl.h;
using static EZ.Http.curl.h.libcurl;

/*
https://www.youtube.com/watch?v=7wL1NyqLs5E
#PromentalshitbackwashpsychosisEnemaSquad
*/

namespace EZ.Http;

public record class EZHttpRequest
{
    public string Method { get; }
    public Uri Uri { get; }
    public Uri? Proxy { get; set; }
    public EZHttpHeaders Headers { get; init; } = new() { ["Accept"] = "*/*"};
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(0.0);
    public bool Verbose { get; init; } = false;
    public bool VerifyTlsCertificate { get; init; } = true;
    public bool FollowAutoRedirect { get; init; } = true;

    public EZHttpRequest(
        string method,
        Uri uri)
    {
        if (!Utils.StrCaseInsesEq(uri.Scheme, "http")
        && !Utils.StrCaseInsesEq(uri.Scheme, "https")) {
            throw new ArgumentException(
                "uri scheme is not http",
                nameof(uri)
            );
        }
        Method = method;
        Uri = uri;
    }

    public EZHttpRequest(
        string method,
        string uri)
    {
        if (!Uri.TryCreate(uri, UriKind.Absolute, out var u)) {
            throw new ArgumentException(
                "not a valid uri.",
                nameof(uri)
            );
        }
        if (!Utils.StrCaseInsesEq(u.Scheme, "http")
         && !Utils.StrCaseInsesEq(u.Scheme, "https")) {
            throw new ArgumentException(
                "uri scheme is not http",
                nameof(uri)
            );
        }
        Uri = u;
        Method = method;
    }

}

public class EZHttpResponseBody
{
    private readonly ChannelReader<ReadOnlyMemory<byte>> _channel;
    private int _readers;
    private ReadOnlyMemory<byte> _content;

    public int? ContentLength { get; private set; }
    
    public async Task<ReadOnlyMemory<byte>> ReadContent(
        CancellationToken cancellationToken = default)
    {
        if (!_content.IsEmpty) {
            return _content;
        }

        if (Interlocked.Increment(ref _readers) > 1) {
            throw new InvalidOperationException(
                "Only one read operation is permitted."
            );
        }
        
        using var ms = new MemoryStream(ContentLength ?? 10969);

        var bodyDataSequence = 
            _channel.ReadAllAsync(
                cancellationToken
            ).ConfigureAwait(false);

        await foreach (var bodyData in bodyDataSequence) {
            ms.Write(bodyData.Span);
        }

        _content = ms.ToArray();
        ContentLength = _content.Length;
        return _content;
    }

    public async Task<string> ReadContentAsString(
        CancellationToken cancellationToken = default
    ) => 
        Encoding.UTF8.GetString(
            (await ReadContent(cancellationToken).ConfigureAwait(false)).Span
        );

    internal EZHttpResponseBody(
        ChannelReader<ReadOnlyMemory<byte>> channel,
        int? length
    ) => (_channel, ContentLength) = (channel, length);
}

public record class EZHttpResponse(
    EZHttpStatusCode Status,
    EZHttpHeaders Headers,
    EZHttpResponseBody Body,
    EZHttpRequest Request
);

public readonly record struct EZConductorConnectionReuseCfg(
/*  
    This is the size of the multi handle's connection cache. Basically that is the
    max number of connections that can be kept alive after a request has completed
    - but it should be noted that also running connections are part of the cache
    so if MAXCONNECTS is smaller than the number of active transfers, there will
    be no idle connections left in the cache after a transfer (thus effectively
    killing connection reuse).
*/
    int MaxKeepAlives,
/*
    limits the number of connections this handle will use to the same host
    name. 
*/
    int MaxKeepAlivesPerHost,
/*
    This limits the total number of *active* connections the multi handle will
    use. Attempting to create more, with additional transfers, will put those
    transfer in an internal waiting queue, waiting for a connection "slot" to
    become available. 
*/
    int MaxTotalConnections
);

public class EZHttpConductor : IAsyncDisposable
{
    private readonly SemaphoreSlim _lock;
    private readonly CURLM _multi;
    private readonly Func<CURL> _acquireCURL;
    private readonly Action<CURL> _freeCURL;
    private readonly CancellationTokenSource _ioLoopCts;
    private readonly AsyncOperation<unit> _ioLoop;

    private bool _disposed;

    public async Task<EZHttpResponse> RequestResponse(
        EZHttpRequest req,
        CancellationToken cancellationToken=default)
    {
        return await EZHttpConduct.RequestResponse(
            _acquireCURL,
            _freeCURL,
            req,
            _multi,
            _lock,
            cancellationToken
        ).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) {
            return;
        }
        _disposed = true;

        _ioLoopCts.Cancel();
        CurlMulti.WakeUp(_multi);

        /* await io loop thread to return before disposing of resources. */
        unit _ = 
            await _ioLoop.Compute()
                .ConfigureAwait(false);

        _ioLoopCts.Dispose();
        _lock.Dispose();
        _multi.Dispose();
    }

    public EZHttpConductor(
        Func<CURL> acuireCURL,
        Action<CURL> freeCURL,
        in EZConductorConnectionReuseCfg connectCfg)
    {
        _acquireCURL = acuireCURL;
        _freeCURL = freeCURL;
        _lock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        _multi = CurlMulti.Alloc();
        _ioLoop = new();
        _ioLoopCts = new();

        EZHttpConduct.BeginProcIOThread(
            connectCfg,
            _multi,
            _lock,
            _ioLoopCts.Token,
            _ioLoop
        );
    }
}

public static class EZHttp
{
    public static async Task<EZHttpResponse> RequestResponseAsync(
        EZHttpConductor conductor,
        EZHttpRequest req,
        CancellationToken cancellationToken)
    {
        return await conductor.RequestResponse(
            req, cancellationToken
        ).ConfigureAwait(false);
    }

    public static void RequestResponse(
        EZHttpRequest request,
        Func<CURL> acquireCURL,
        Action<CURL> freeCURL)
    {
        using var xfer =
            new EZHttpXfer(
                request,
                acquireCURL(),
                freeCURL
            );

        EZHttpConduct.ConfigureCURLoptions(xfer);
        CURLcode code = curl_easy_perform(xfer.EZ);
        CurlEz.ThrowIfErr(code);
    }

    public static void RequestResponse(
        EZHttpRequest request,
        CURL easy)
    {
        RequestResponse(
            request,
            () => easy,
            _ => {/* caller should dispose */}
        );
    }

    public static void RequestResponse(
        EZHttpRequest request)
    {
        RequestResponse(
            request,
            CurlEz.Alloc,
            CurlEz.Dispose
        );
    }
}