using EZ.Http.curl.h;

namespace EZ.Http;


public class CurlException : Exception
{
    public CurlException(string? message) : base(message)
    {
    }
}

public class CurlCodeException : CurlException
{
    public CurlCodeException(string? message) : base(message)
    {
    }

    public CurlCodeException(CURLcode code) : this(CurlEz.StrError(code))
    {
    }

    public CurlCodeException(CURLcode code, string? secondaryMsg)
        : this($"{CurlEz.StrError(code)} {secondaryMsg}")
    {
    }
}

public class CurlMultiCodeException : CurlException
{
    public CurlMultiCodeException(string? message) : base(message)
    {
    }

    public CurlMultiCodeException(CURLMcode code) : this($"{CurlMulti.StrError(code)}")
    {
    }

    public CurlMultiCodeException(CURLMcode code,string? secondaryMsg)
        : this($"{CurlMulti.StrError(code)}{" " + secondaryMsg ?? ""}")
    {
    }
}

