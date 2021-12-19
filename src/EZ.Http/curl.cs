using System.Runtime.InteropServices;
using EZ.Http.curl.h;
using static EZ.Http.curl.h.libcurl;

namespace EZ.Http;

public abstract class HeapAllocatedResource :
    IEquatable<HeapAllocatedResource>, IDisposable
{
    private bool _disposed;
    private readonly nint _ptrToResrc;
    private readonly Action<nint> _free;

    protected virtual void Dispose(
        bool disposing)
    {
        if (_disposed) {
            return;
        }
        _disposed = true;

        if (disposing) {
            /*dispose managed resources*/
        }

        _free(_ptrToResrc);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public bool Equals(
        HeapAllocatedResource? other
    ) =>
        other != null
        && _ptrToResrc == other._ptrToResrc;

    public static implicit operator nint(
        HeapAllocatedResource resource)
    {
        if (resource._disposed) {
            throw new ObjectDisposedException(
                resource.GetType().Name
            );
        }
        return resource._ptrToResrc;
    }

    protected HeapAllocatedResource(
        Func<nint> alloc,
        Action<nint> free)
    {
        _ptrToResrc = alloc();
        if (IntPtr.Zero == _ptrToResrc) {
            throw new OutOfMemoryException(
                "heap resource allocate function returned NULL."
            );
        }
        _free = free;
    }

    protected HeapAllocatedResource(
        nint ptr,
        Action<nint> free)
    {
        if (IntPtr.Zero == ptr) {
            throw new ArgumentException(
                "HeapResource does not permit NULL values."
            );
        }
        _ptrToResrc = ptr;
        _free = free;
    }


    ~HeapAllocatedResource() => Dispose(disposing: false);
}

public class CURL : HeapAllocatedResource
{ 
    internal CURL() : base(
        alloc: curl_easy_init,
        free: curl_easy_cleanup
    ) { } 
}

public class curl_slist : HeapAllocatedResource
{
    internal curl_slist() : base(
        alloc: () => curl_slist_append(IntPtr.Zero, ""),
        free: curl_slist_free_all
    ) { }
}

public class CURLM : HeapAllocatedResource
{
    internal CURLM() : base(
        alloc: curl_multi_init,
        free: multi => {
            CURLMcode m = curl_multi_cleanup(multi);
            if (CURLMcode.CURLM_OK != m) {
                throw new CurlMultiCodeException(
                    m
                );
            }
        }
    ) { }
}

public static class CurlEz
{
    public static string StrError(
        CURLcode code
    ) =>
        Marshal.PtrToStringAnsi(
            curl_easy_strerror(code)
        ) ?? throw new InvalidOperationException(
            "curl_easy_strerror returned NULL."
        );

    public static void ThrowIfErr(
        CURLcode code)
    {
        if (code == CURLcode.CURLE_OK) {
            return;
        }
        throw new CurlCodeException(code);
    }

    public static CURL Alloc() =>
        new();

    public static void Free(
        CURL a
    ) => a.Dispose();

    public static void Reset(
        CURL easy
    ) => curl_easy_reset(easy);

    public static T Info<T>(
        nint easy,
        CURLINFO info)
    {
        (CURLcode, T) GetInt() => (
            curl_easy_getinfo(easy, info, out int i),
            (T)Convert.ChangeType(i, typeof(T))
        );
        
        (CURLcode, T) GetNInt() => (
            curl_easy_getinfo(easy, info, out nint i),
            (T)Convert.ChangeType(i, typeof(T))
        );
        
        (CURLcode, T) GetStr()
        {
            var code = curl_easy_getinfo(easy, info, out nint ptr);
            var str = Marshal.PtrToStringAnsi(ptr);
            return (code, (T)Convert.ChangeType(str, typeof(T))!);
        }

        var (code, ret) =
            typeof(T).Name switch {
                nameof(Int32) => GetInt(),
                nameof(String) => GetStr(),
                nameof(IntPtr) => GetNInt(),
                _ => throw new ArgumentException(
                    "invalid generic type"
                )
            };
        ThrowIfErr(code);
        return ret;
    }

    public static void SetOpt<T>(
        CURL easy,
        CURLoption opt,
        T value)
    {
        var code =
            value switch {
                string s => curl_easy_setopt(easy, opt, s),
                bool b => curl_easy_setopt(easy, opt, b ? 1 : 0),
                long l => curl_easy_setopt(easy, opt, l),
                nint n => curl_easy_setopt(easy, opt, n),
                int i => curl_easy_setopt(easy, opt, i),
                null => curl_easy_setopt(easy, opt, str: null),
                _ => throw new InvalidOperationException($"invalid curl option type {typeof(T).Name}")
            };
        ThrowIfErr(code);
    }
}

public static class CurlSList
{
    public static curl_slist Alloc() =>
        new();

    /// <summary>
    /// Appends a string to a linked list. If no list exists, it will be created
    /// first.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="value"></param>
    public static void Append(
        curl_slist list,
        string value)
    {
        nint tmp = curl_slist_append(list, value);
        if (IntPtr.Zero == tmp) {
            throw new InvalidOperationException(
                "call to malloc in curl_slist_append returned NULL."
            );
        }
        if (tmp != list) {
            throw new InvalidOperationException(
                "curl_slist_append returned new pointer."
            );
        }
    }
}

public static class CurlMulti
{
    public static string StrError(
        CURLMcode code
    ) =>
        Marshal.PtrToStringAnsi(
            curl_multi_strerror(code)
        ) ?? throw new InvalidOperationException(
            "curl_multi_strerror returned NULL."
        );

    public static CURLM Alloc() =>
        new();

    public static void ThrowIfErr(
        CURLMcode code)
    {
        if (code == CURLMcode.CURLM_OK) {
            return;
        }
        throw new CurlMultiCodeException(code);
    }

    public static void Free(
        CURLM multi)
    {
        var code = curl_multi_cleanup(multi);
        ThrowIfErr(code);
    }

    /// <summary>
    /// add a standard curl handle to the multi stack
    /// </summary>
    /// <param name="multi"></param>
    /// <param name="curl"></param>
    public static void Add(
        CURLM multi,
        CURL curl)
    {
        var code = curl_multi_add_handle(multi, curl);
        ThrowIfErr(code);
    }

    /// <summary>
    /// removes a curl handle from the multi stack again
    /// </summary>
    /// <param name="multi"></param>
    /// <param name="curl"></param>
    public static void Remove(
        CURLM multi,
        nint curl)
    {
        var code = curl_multi_remove_handle(multi, curl);
        ThrowIfErr(code);
    }

    /// <summary>
    /// When the app thinks there's data available for curl it calls this
    /// function to read/write whatever there is right now. This returns
    /// as soon as the reads and writes are done. This function does not
    /// require that there actually is data available for reading or that
    /// data can be written, it can be called just in case.
    /// </summary>
    /// <param name="multi"></param>
    /// <returns>
    /// the number of handles that still transfer data in the second
    /// argument's integer-pointer.
    /// </returns>
    public static bool Perform(
        CURLM multi,
        out int active)
    {
        var code = curl_multi_perform(multi, out active);
        ThrowIfErr(code);
        return active > 0;
    }

    /// <summary>
    /// Poll on all fds within a CURLM.
    /// </summary>
    /// <param name="multi"></param>
    /// <param name="timeoutMs"></param>
    public static int Poll(
        CURLM multi,
        int timeoutMs)
    {
        var code =
            curl_multi_poll(
                multi,
                IntPtr.Zero,
                0,
                timeoutMs,
                out var i
            );
        ThrowIfErr(code);
        return i;
    }

    public static void WakeUp(
        CURLM multi)
    {
        var code = curl_multi_wakeup(multi);
        ThrowIfErr(code);
    }

    public static bool ReadInfo(
        CURLM multi,
        out CURLMsg msg)
    {
        msg = default;
        var ptr = curl_multi_info_read(multi, out var _queueCnt);
        if (IntPtr.Zero == ptr) {
            return false;
        }
        msg = Marshal.PtrToStructure<CURLMsg>(ptr);
        return true;
    }

    public static void SetOpt<T>(
        CURLM multi,
        CURLMoption opt,
        T value)
    {
        var code =
            value switch {
                string s => curl_multi_setopt(multi, opt, s),
                bool b => curl_multi_setopt(multi, opt, b ? 1 : 0),
                long l => curl_multi_setopt(multi, opt, l),
                nint n => curl_multi_setopt(multi, opt, n),
                int i => curl_multi_setopt(multi, opt, i),
                null => curl_multi_setopt(multi, opt, str: null),
                _ => throw new InvalidOperationException(
                    $"invalid curl option type {typeof(T).Name}"
                )
            };
        ThrowIfErr(code);
    }
}