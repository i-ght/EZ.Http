using System.Runtime.InteropServices;

namespace EZ.Http.curl.h;

/* https://github.com/curl/curl/blob/master/include/curl/curl.h */

public enum CURLcode
{
    CURLE_OK = 0,
    CURLE_UNSUPPORTED_PROTOCOL,    /* 1 */
    CURLE_FAILED_INIT,              /* 2 */
    CURLE_URL_MALFORMAT,            /* 3 */
    CURLE_NOT_BUILT_IN,             /* 4 - [was obsoleted in August 2007 for
                                         7.17.0, reused in April 2011 for 7.21.5] */
    CURLE_COULDNT_RESOLVE_PROXY,   /* 5 */
    CURLE_COULDNT_RESOLVE_HOST,    /* 6 */
    CURLE_COULDNT_CONNECT,          /* 7 */
    CURLE_WEIRD_SERVER_REPLY,      /* 8 */
    CURLE_REMOTE_ACCESS_DENIED,    /* 9 a service was denied by the server
                                         due to lack of access - when login fails
                                         this is not returned. */
    CURLE_FTP_ACCEPT_FAILED,        /* 10 - [was obsoleted in April 2006 for
                                         7.15.4, reused in Dec 2011 for 7.24.0]*/
    CURLE_FTP_WEIRD_PASS_REPLY,    /* 11 */
    CURLE_FTP_ACCEPT_TIMEOUT,      /* 12 - timeout occurred accepting server
                                         [was obsoleted in August 2007 for 7.17.0,
                                         reused in Dec 2011 for 7.24.0]*/
    CURLE_FTP_WEIRD_PASV_REPLY,    /* 13 */
    CURLE_FTP_WEIRD_227_FORMAT,    /* 14 */
    CURLE_FTP_CANT_GET_HOST,        /* 15 */
    CURLE_HTTP2,                     /* 16 - A problem in the http2 framing layer.
                                         [was obsoleted in August 2007 for 7.17.0,
                                         reused in July 2014 for 7.38.0] */
    CURLE_FTP_COULDNT_SET_TYPE,    /* 17 */
    CURLE_PARTIAL_FILE,             /* 18 */
    CURLE_FTP_COULDNT_RETR_FILE,   /* 19 */
    CURLE_OBSOLETE20,                /* 20 - NOT USED */
    CURLE_QUOTE_ERROR,              /* 21 - quote command failure */
    CURLE_HTTP_RETURNED_ERROR,     /* 22 */
    CURLE_WRITE_ERROR,              /* 23 */
    CURLE_OBSOLETE24,                /* 24 - NOT USED */
    CURLE_UPLOAD_FAILED,            /* 25 - failed upload "command" */
    CURLE_READ_ERROR,                /* 26 - couldn't open/read from file */
    CURLE_OUT_OF_MEMORY,            /* 27 */
    /* Note: CURLE_OUT_OF_MEMORY may sometimes indicate a conversion error
             instead of a memory allocation error if CURL_DOES_CONVERSIONS
             is defined
    */
    CURLE_OPERATION_TIMEDOUT,      /* 28 - the timeout time was reached */
    CURLE_OBSOLETE29,                /* 29 - NOT USED */
    CURLE_FTP_PORT_FAILED,          /* 30 - FTP PORT operation failed */
    CURLE_FTP_COULDNT_USE_REST,    /* 31 - the REST command failed */
    CURLE_OBSOLETE32,                /* 32 - NOT USED */
    CURLE_RANGE_ERROR,              /* 33 - RANGE "command" didn't work */
    CURLE_HTTP_POST_ERROR,          /* 34 */
    CURLE_SSL_CONNECT_ERROR,        /* 35 - wrong when connecting with SSL */
    CURLE_BAD_DOWNLOAD_RESUME,     /* 36 - couldn't resume download */
    CURLE_FILE_COULDNT_READ_FILE,  /* 37 */
    CURLE_LDAP_CANNOT_BIND,         /* 38 */
    CURLE_LDAP_SEARCH_FAILED,      /* 39 */
    CURLE_OBSOLETE40,                /* 40 - NOT USED */
    CURLE_FUNCTION_NOT_FOUND,      /* 41 - NOT USED starting with 7.53.0 */
    CURLE_ABORTED_BY_CALLBACK,     /* 42 */
    CURLE_BAD_FUNCTION_ARGUMENT,   /* 43 */
    CURLE_OBSOLETE44,                /* 44 - NOT USED */
    CURLE_INTERFACE_FAILED,         /* 45 - CURLOPT_INTERFACE failed */
    CURLE_OBSOLETE46,                /* 46 - NOT USED */
    CURLE_TOO_MANY_REDIRECTS,      /* 47 - catch endless re-direct loops */
    CURLE_UNKNOWN_OPTION,           /* 48 - User specified an unknown option */
    CURLE_SETOPT_OPTION_SYNTAX,    /* 49 - Malformed setopt option */
    CURLE_OBSOLETE50,                /* 50 - NOT USED */
    CURLE_OBSOLETE51,                /* 51 - NOT USED */
    CURLE_GOT_NOTHING,              /* 52 - when this is a specific error */
    CURLE_SSL_ENGINE_NOTFOUND,     /* 53 - SSL crypto engine not found */
    CURLE_SSL_ENGINE_SETFAILED,    /* 54 - can not set SSL crypto engine as
                                         default */
    CURLE_SEND_ERROR,                /* 55 - failed sending network data */
    CURLE_RECV_ERROR,                /* 56 - failure in receiving network data */
    CURLE_OBSOLETE57,                /* 57 - NOT IN USE */
    CURLE_SSL_CERTPROBLEM,          /* 58 - problem with the local certificate */
    CURLE_SSL_CIPHER,                /* 59 - couldn't use specified cipher */
    CURLE_PEER_FAILED_VERIFICATION, /* 60 - peer's certificate or fingerprint
                                             wasn't verified fine */
    CURLE_BAD_CONTENT_ENCODING,    /* 61 - Unrecognized/bad encoding */
    CURLE_LDAP_INVALID_URL,         /* 62 - Invalid LDAP URL */
    CURLE_FILESIZE_EXCEEDED,        /* 63 - Maximum file size exceeded */
    CURLE_USE_SSL_FAILED,           /* 64 - Requested FTP SSL level failed */
    CURLE_SEND_FAIL_REWIND,         /* 65 - Sending the data requires a rewind
                                         that failed */
    CURLE_SSL_ENGINE_INITFAILED,   /* 66 - failed to initialise ENGINE */
    CURLE_LOGIN_DENIED,             /* 67 - user, password or similar was not
                                         accepted and we failed to login */
    CURLE_TFTP_NOTFOUND,            /* 68 - file not found on server */
    CURLE_TFTP_PERM,                 /* 69 - permission problem on server */
    CURLE_REMOTE_DISK_FULL,         /* 70 - out of disk space on server */
    CURLE_TFTP_ILLEGAL,             /* 71 - Illegal TFTP operation */
    CURLE_TFTP_UNKNOWNID,           /* 72 - Unknown transfer ID */
    CURLE_REMOTE_FILE_EXISTS,      /* 73 - File already exists */
    CURLE_TFTP_NOSUCHUSER,          /* 74 - No such user */
    CURLE_CONV_FAILED,              /* 75 - conversion failed */
    CURLE_CONV_REQD,                 /* 76 - caller must register conversion
                                         callbacks using curl_easy_setopt options
                                         CURLOPT_CONV_FROM_NETWORK_FUNCTION,
                                         CURLOPT_CONV_TO_NETWORK_FUNCTION, and
                                         CURLOPT_CONV_FROM_UTF8_FUNCTION */
    CURLE_SSL_CACERT_BADFILE,      /* 77 - could not load CACERT file, missing
                                         or wrong format */
    CURLE_REMOTE_FILE_NOT_FOUND,   /* 78 - remote file not found */
    CURLE_SSH,                        /* 79 - error from the SSH layer, somewhat
                                         generic so the error message will be of
                                         interest when this has happened */

    CURLE_SSL_SHUTDOWN_FAILED,     /* 80 - Failed to shut down the SSL
                                         connection */
    CURLE_AGAIN,                     /* 81 - socket is not ready for send/recv,
                                         wait till it's ready and try again (Added
                                         in 7.18.2) */
    CURLE_SSL_CRL_BADFILE,          /* 82 - could not load CRL file, missing or
                                         wrong format (Added in 7.19.0) */
    CURLE_SSL_ISSUER_ERROR,         /* 83 - Issuer check failed.  (Added in
                                         7.19.0) */
    CURLE_FTP_PRET_FAILED,          /* 84 - a PRET command failed */
    CURLE_RTSP_CSEQ_ERROR,          /* 85 - mismatch of RTSP CSeq numbers */
    CURLE_RTSP_SESSION_ERROR,      /* 86 - mismatch of RTSP Session Ids */
    CURLE_FTP_BAD_FILE_LIST,        /* 87 - unable to parse FTP file list */
    CURLE_CHUNK_FAILED,             /* 88 - chunk callback reported error */
    CURLE_NO_CONNECTION_AVAILABLE, /* 89 - No connection available, the
                                         session will be queued */
    CURLE_SSL_PINNEDPUBKEYNOTMATCH, /* 90 - specified pinned public key did not
                                             match */
    CURLE_SSL_INVALIDCERTSTATUS,   /* 91 - invalid certificate status */
    CURLE_HTTP2_STREAM,             /* 92 - stream error in HTTP/2 framing layer
                                         */
    CURLE_RECURSIVE_API_CALL,      /* 93 - an api function was called from
                                         inside a callback */
    CURLE_AUTH_ERROR,                /* 94 - an authentication function returned an
                                         error */
    CURLE_HTTP3,                     /* 95 - An HTTP/3 layer problem */
    CURLE_QUIC_CONNECT_ERROR,      /* 96 - QUIC connection error */
    CURLE_PROXY,                     /* 97 - proxy handshake error */
    CURLE_SSL_CLIENTCERT,           /* 98 - client-side certificate required */
    CURL_LAST /* never use! */
}

public enum CURLINFO
{
    CURLINFO_NONE, /* first, never use this */
    CURLINFO_EFFECTIVE_URL = 0x100000 + 1,
    CURLINFO_RESPONSE_CODE = 0x200000 + 2,
    CURLINFO_TOTAL_TIME = 0x300000 + 3,
    CURLINFO_NAMELOOKUP_TIME = 0x300000 + 4,
    CURLINFO_CONNECT_TIME = 0x300000 + 5,
    CURLINFO_PRETRANSFER_TIME = 0x300000 + 6,
    CURLINFO_SIZE_UPLOAD = 0x300000 + 7,
    CURLINFO_SIZE_UPLOAD_T = 0x600000 + 7,
    CURLINFO_SIZE_DOWNLOAD = 0x300000 + 8,
    CURLINFO_SIZE_DOWNLOAD_T = 0x600000 + 8,
    CURLINFO_SPEED_DOWNLOAD = 0x300000 + 9,
    CURLINFO_SPEED_DOWNLOAD_T = 0x600000 + 9,
    CURLINFO_SPEED_UPLOAD = 0x300000 + 10,
    CURLINFO_SPEED_UPLOAD_T = 0x600000 + 10,
    CURLINFO_HEADER_SIZE = 0x200000 + 11,
    CURLINFO_REQUEST_SIZE = 0x200000 + 12,
    CURLINFO_SSL_VERIFYRESULT = 0x200000 + 13,
    CURLINFO_FILETIME = 0x200000 + 14,
    CURLINFO_FILETIME_T = 0x600000 + 14,
    CURLINFO_CONTENT_LENGTH_DOWNLOAD = 0x300000 + 15,
    CURLINFO_CONTENT_LENGTH_DOWNLOAD_T = 0x600000 + 15,
    CURLINFO_CONTENT_LENGTH_UPLOAD = 0x300000 + 16,
    CURLINFO_CONTENT_LENGTH_UPLOAD_T = 0x600000 + 16,
    CURLINFO_STARTTRANSFER_TIME = 0x300000 + 17,
    CURLINFO_CONTENT_TYPE = 0x100000 + 18,
    CURLINFO_REDIRECT_TIME = 0x300000 + 19,
    CURLINFO_REDIRECT_COUNT = 0x200000 + 20,
    CURLINFO_PRIVATE = 0x100000 + 21,
    CURLINFO_HTTP_CONNECTCODE = 0x200000 + 22,
    CURLINFO_HTTPAUTH_AVAIL = 0x200000 + 23,
    CURLINFO_PROXYAUTH_AVAIL = 0x200000 + 24,
    CURLINFO_OS_ERRNO = 0x200000 + 25,
    CURLINFO_NUM_CONNECTS = 0x200000 + 26,
    CURLINFO_SSL_ENGINES = 0x400000 + 27,
    CURLINFO_COOKIELIST = 0x400000 + 28,
    CURLINFO_LASTSOCKET = 0x200000 + 29,
    CURLINFO_FTP_ENTRY_PATH = 0x100000 + 30,
    CURLINFO_REDIRECT_URL = 0x100000 + 31,
    CURLINFO_PRIMARY_IP = 0x100000 + 32,
    CURLINFO_APPCONNECT_TIME = 0x300000 + 33,
    CURLINFO_CERTINFO = 0x400000 + 34,
    CURLINFO_CONDITION_UNMET = 0x200000 + 35,
    CURLINFO_RTSP_SESSION_ID = 0x100000 + 36,
    CURLINFO_RTSP_CLIENT_CSEQ = 0x200000 + 37,
    CURLINFO_RTSP_SERVER_CSEQ = 0x200000 + 38,
    CURLINFO_RTSP_CSEQ_RECV = 0x200000 + 39,
    CURLINFO_PRIMARY_PORT = 0x200000 + 40,
    CURLINFO_LOCAL_IP = 0x100000 + 41,
    CURLINFO_LOCAL_PORT = 0x200000 + 42,
    CURLINFO_TLS_SESSION = 0x400000 + 43,
    CURLINFO_ACTIVESOCKET = 0x500000 + 44,
    CURLINFO_TLS_SSL_PTR = 0x400000 + 45,
    CURLINFO_HTTP_VERSION = 0x200000 + 46,
    CURLINFO_PROXY_SSL_VERIFYRESULT = 0x200000 + 47,
    CURLINFO_PROTOCOL = 0x200000 + 48,
    CURLINFO_SCHEME = 0x100000 + 49,
    CURLINFO_TOTAL_TIME_T = 0x600000 + 50,
    CURLINFO_NAMELOOKUP_TIME_T = 0x600000 + 51,
    CURLINFO_CONNECT_TIME_T = 0x600000 + 52,
    CURLINFO_PRETRANSFER_TIME_T = 0x600000 + 53,
    CURLINFO_STARTTRANSFER_TIME_T = 0x600000 + 54,
    CURLINFO_REDIRECT_TIME_T = 0x600000 + 55,
    CURLINFO_APPCONNECT_TIME_T = 0x600000 + 56,
    CURLINFO_RETRY_AFTER = 0x600000 + 57,
    CURLINFO_EFFECTIVE_METHOD = 0x100000 + 58,
    CURLINFO_PROXY_ERROR = 0x200000 + 59,
    CURLINFO_REFERER = 0x100000 + 60,

    CURLINFO_LASTONE = 60
}

internal static class CURLOPTTYPE
{
    public const int LONG = 0;
    public const int OBJECTPOINT = 10000;
    public const int FUNCTIONPOINT = 20000;
    public const int OFF_T = 30000;
    public const int BLOB = 40000;
    public const int STRINGPOINT = OBJECTPOINT;
    public const int SLISTPOINT = OBJECTPOINT;
    public const int CBPOINT = OBJECTPOINT;
    public const int VALUES = LONG;
}

public enum CURLoption
{
    /* This is the FILE * or void * the regular output should be written to. */
    CURLOPT_WRITEDATA = CURLOPTTYPE.CBPOINT + 1,

    /* The full URL to get/put */
    CURLOPT_URL = CURLOPTTYPE.STRINGPOINT + 2,

    /* Port number to connect to, if other than default. */
    CURLOPT_PORT = CURLOPTTYPE.LONG + 3,

    /* Name of proxy to use. */
    CURLOPT_PROXY = CURLOPTTYPE.STRINGPOINT + 4,

    /* "user:password;options" to use when fetching. */
    CURLOPT_USERPWD = CURLOPTTYPE.STRINGPOINT + 5,

    /* "user:password" to use with proxy. */
    CURLOPT_PROXYUSERPWD = CURLOPTTYPE.STRINGPOINT + 6,

    /* Range to get, specified as an ASCII string. */
    CURLOPT_RANGE = CURLOPTTYPE.STRINGPOINT + 7,

    /* not used */

    /* Specified file stream to upload from (use as input): */
    CURLOPT_READDATA = CURLOPTTYPE.CBPOINT + 9,

    /* Buffer to receive error messages in, must be at least CURL_ERROR_SIZE
     * bytes big. */
    CURLOPT_ERRORBUFFER = CURLOPTTYPE.OBJECTPOINT + 10,

    /* Function that will be called to store the output (instead of fwrite). The
     * parameters will use fwrite() syntax, make sure to follow them. */
    CURLOPT_WRITEFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 11,

    /* Function that will be called to read the input (instead of fread). The
     * parameters will use fread() syntax, make sure to follow them. */
    CURLOPT_READFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 12,

    /* Time-out the read operation after this amount of seconds */
    CURLOPT_TIMEOUT = CURLOPTTYPE.LONG + 13,

    /* If the CURLOPT_INFILE is used, this can be used to inform libcurl about
     * how large the file being sent really is. That allows better error
     * checking and better verifies that the upload was successful. -1 means
     * unknown size.
     *
     * For large file support, there is also a _LARGE version of the key
     * which takes an off_t type, allowing platforms with larger off_t
     * sizes to easy larger files.  See below for INFILESIZE_LARGE.
     */
    CURLOPT_INFILESIZE = CURLOPTTYPE.LONG + 14,

    /* POST static input fields. */
    CURLOPT_POSTFIELDS = CURLOPTTYPE.OBJECTPOINT + 15,

    /* Set the referrer page (needed by some CGIs) */
    CURLOPT_REFERER = CURLOPTTYPE.STRINGPOINT + 16,

    /* Set the FTP PORT string (interface name, named or numerical IP address)
        Use i.e '-' to use default address. */
    CURLOPT_FTPPORT = CURLOPTTYPE.STRINGPOINT + 17,

    /* Set the User-Agent string (examined by some CGIs) */
    CURLOPT_USERAGENT = CURLOPTTYPE.STRINGPOINT + 18,

    /* If the download receives less than "low speed limit" bytes/second
     * during "low speed time" seconds, the operations is aborted.
     * You could i.e if you have a pretty high speed connection, abort if
     * it is less than 2000 bytes/sec during 20 seconds.
     */

    /* Set the "low speed limit" */
    CURLOPT_LOW_SPEED_LIMIT = CURLOPTTYPE.LONG + 19,

    /* Set the "low speed time" */
    CURLOPT_LOW_SPEED_TIME = CURLOPTTYPE.LONG + 20,

    /* Set the continuation offset.
     *
     * Note there is also a _LARGE version of this key which uses
     * off_t types, allowing for large file offsets on platforms which
     * use larger-than-32-bit off_t's.  Look below for RESUME_FROM_LARGE.
     */
    CURLOPT_RESUME_FROM = CURLOPTTYPE.LONG + 21,

    /* Set cookie in request: */
    CURLOPT_COOKIE = CURLOPTTYPE.STRINGPOINT + 22,

    /* This points to a linked list of headers, struct curl_slist kind. This
        list is also used for RTSP (in spite of its name) */
    CURLOPT_HTTPHEADER = CURLOPTTYPE.SLISTPOINT + 23,

    /* This points to a linked list of post entries, struct curl_httppost */
    CURLOPT_HTTPPOST = CURLOPTTYPE.OBJECTPOINT + 24,

    /* name of the file keeping your private SSL-certificate */
    CURLOPT_SSLCERT = CURLOPTTYPE.STRINGPOINT + 25,

    /* password for the SSL or SSH private key */
    CURLOPT_KEYPASSWD = CURLOPTTYPE.STRINGPOINT + 26,

    /* send TYPE parameter? */
    CURLOPT_CRLF = CURLOPTTYPE.LONG + 27,

    /* send linked-list of QUOTE commands */
    CURLOPT_QUOTE = CURLOPTTYPE.SLISTPOINT + 28,

    /* send FILE * or void * to store headers to, if you use a callback it
        is simply passed to the callback unmodified */
    CURLOPT_HEADERDATA = CURLOPTTYPE.CBPOINT + 29,

    /* point to a file to read the initial cookies from, also enables
        "cookie awareness" */
    CURLOPT_COOKIEFILE = CURLOPTTYPE.STRINGPOINT + 31,

    /* What version to specifically try to use.
        See CURL_SSLVERSION defines below. */
    CURLOPT_SSLVERSION = CURLOPTTYPE.VALUES + 32,

    /* What kind of HTTP time condition to use, see defines */
    CURLOPT_TIMECONDITION = CURLOPTTYPE.VALUES + 33,

    /* Time to use with the above condition. Specified in number of seconds
        since 1 Jan 1970 */
    CURLOPT_TIMEVALUE = CURLOPTTYPE.LONG + 34,

    /* 35 = OBSOLETE */

    /* Custom request, for customizing the get command like
        HTTP: DELETE, TRACE and others
        FTP: to use a different list command
        */
    CURLOPT_CUSTOMREQUEST = CURLOPTTYPE.STRINGPOINT + 36,

    /* FILE easy to use instead of stderr */
    CURLOPT_STDERR = CURLOPTTYPE.OBJECTPOINT + 37,

    /* 38 is not used */

    /* send linked-list of post-transfer QUOTE commands */
    CURLOPT_POSTQUOTE = CURLOPTTYPE.SLISTPOINT + 39,

    /* OBSOLETE, do not use! */
    CURLOPT_OBSOLETE40 = CURLOPTTYPE.OBJECTPOINT + 40,

    /* talk a lot */
    CURLOPT_VERBOSE = CURLOPTTYPE.LONG + 41,

    /* throw the header out too */
    CURLOPT_HEADER = CURLOPTTYPE.LONG + 42,

    /* shut off the progress meter */
    CURLOPT_NOPROGRESS = CURLOPTTYPE.LONG + 43,

    /* use HEAD to get http document */
    CURLOPT_NOBODY = CURLOPTTYPE.LONG + 44,

    /* no output on http error codes >= 400 */
    CURLOPT_FAILONERROR = CURLOPTTYPE.LONG + 45,

    /* this is an upload */
    CURLOPT_UPLOAD = CURLOPTTYPE.LONG + 46,

    /* HTTP POST method */
    CURLOPT_POST = CURLOPTTYPE.LONG + 47,

    /* bare names when listing directories */
    CURLOPT_DIRLISTONLY = CURLOPTTYPE.LONG + 48,

    /* Append instead of overwrite on upload! */
    CURLOPT_APPEND = CURLOPTTYPE.LONG + 50,

    /* Specify whether to read the user+password from the .netrc or the URL.
     * This must be one of the CURL_NETRC_* enums below. */
    CURLOPT_NETRC = CURLOPTTYPE.VALUES + 51,

    /* use Location: Luke! */
    CURLOPT_FOLLOWLOCATION = CURLOPTTYPE.LONG + 52,

    /* transfer data in text/ASCII format */
    CURLOPT_TRANSFERTEXT = CURLOPTTYPE.LONG + 53,

    /* HTTP PUT */
    CURLOPT_PUT = CURLOPTTYPE.LONG + 54,

    /* 55 = OBSOLETE */

    /* DEPRECATED
     * Function that will be called instead of the internal progress display
     * function. This function should be defined as the curl_progress_callback
     * prototype defines. */
    CURLOPT_PROGRESSFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 56,

    /* Data passed to the CURLOPT_PROGRESSFUNCTION and CURLOPT_XFERINFOFUNCTION
        callbacks */
    CURLOPT_XFERINFODATA = CURLOPTTYPE.CBPOINT + 57,
    /*#define CURLOPT_PROGRESSDATA CURLOPT_XFERINFODATA*/

    /* We want the referrer field set automatically when following locations */
    CURLOPT_AUTOREFERER = CURLOPTTYPE.LONG + 58,

    /* Port of the proxy, can be set in the proxy string as well with:
        "[host]:[port]" */
    CURLOPT_PROXYPORT = CURLOPTTYPE.LONG + 59,

    /* size of the POST input data, if strlen() is not good to use */
    CURLOPT_POSTFIELDSIZE = CURLOPTTYPE.LONG + 60,

    /* tunnel non-http operations through a HTTP proxy */
    CURLOPT_HTTPPROXYTUNNEL = CURLOPTTYPE.LONG + 61,

    /* Set the interface string to use as outgoing network interface */
    CURLOPT_INTERFACE = CURLOPTTYPE.STRINGPOINT + 62,

    /* Set the krb4/5 security level, this also enables krb4/5 awareness.  This
     * is a string, 'clear', 'safe', 'confidential' or 'private'.  If the string
     * is set but doesn't match one of these, 'private' will be used.  */
    CURLOPT_KRBLEVEL = CURLOPTTYPE.STRINGPOINT + 63,

    /* Set if we should verify the peer in ssl handshake, set 1 to verify. */
    CURLOPT_SSL_VERIFYPEER = CURLOPTTYPE.LONG + 64,

    /* The CApath or CAfile used to validate the peer certificate
        this option is used only if SSL_VERIFYPEER is true */
    CURLOPT_CAINFO = CURLOPTTYPE.STRINGPOINT + 65,

    /* 66 = OBSOLETE */
    /* 67 = OBSOLETE */

    /* Maximum number of http redirects to follow */
    CURLOPT_MAXREDIRS = CURLOPTTYPE.LONG + 68,

    /* Pass a long set to 1 to get the date of the requested document (if
        possible)! Pass a zero to shut it off. */
    CURLOPT_FILETIME = CURLOPTTYPE.LONG + 69,

    /* This points to a linked list of telnet options */
    CURLOPT_TELNETOPTIONS = CURLOPTTYPE.SLISTPOINT + 70,

    /* Max amount of cached alive connections */
    CURLOPT_MAXCONNECTS = CURLOPTTYPE.LONG + 71,

    /* OBSOLETE, do not use! */
    CURLOPT_OBSOLETE72 = CURLOPTTYPE.LONG + 72,

    /* 73 = OBSOLETE */

    /* Set to explicitly use a new connection for the upcoming transfer.
        Do not use this unless you're absolutely sure of this, as it makes the
        operation slower and is less friendly for the network. */
    CURLOPT_FRESH_CONNECT = CURLOPTTYPE.LONG + 74,

    /* Set to explicitly forbid the upcoming transfer's connection to be re-used
        when done. Do not use this unless you're absolutely sure of this, as it
        makes the operation slower and is less friendly for the network. */
    CURLOPT_FORBID_REUSE = CURLOPTTYPE.LONG + 75,

    /* Set to a file name that contains random data for libcurl to use to
        seed the random engine when doing SSL connects. */
    CURLOPT_RANDOM_FILE = CURLOPTTYPE.STRINGPOINT + 76,

    /* Set to the Entropy Gathering Daemon socket pathname */
    CURLOPT_EGDSOCKET = CURLOPTTYPE.STRINGPOINT + 77,

    /* Time-out connect operations after this amount of seconds, if connects are
        OK within this time, then fine... This only aborts the connect phase. */
    CURLOPT_CONNECTTIMEOUT = CURLOPTTYPE.LONG + 78,

    /* Function that will be called to store headers (instead of fwrite). The
     * parameters will use fwrite() syntax, make sure to follow them. */
    CURLOPT_HEADERFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 79,

    /* Set this to force the HTTP request to get back to GET. Only really usable
        if POST, PUT or a custom request have been used first.
     */
    CURLOPT_HTTPGET = CURLOPTTYPE.LONG + 80,

    /* Set if we should verify the Common name from the peer certificate in ssl
     * handshake, set 1 to check existence, 2 to ensure that it matches the
     * provided hostname. */
    CURLOPT_SSL_VERIFYHOST = CURLOPTTYPE.LONG + 81,

    /* Specify which file name to write all known cookies in after completed
        operation. Set file name to "-" (dash) to make it go to stdout. */
    CURLOPT_COOKIEJAR = CURLOPTTYPE.STRINGPOINT + 82,

    /* Specify which SSL ciphers to use */
    CURLOPT_SSL_CIPHER_LIST = CURLOPTTYPE.STRINGPOINT + 83,

    /* Specify which HTTP version to use! This must be set to one of the
        CURL_HTTP_VERSION* enums set below. */
    CURLOPT_HTTP_VERSION = CURLOPTTYPE.VALUES + 84,

    /* Specifically switch on or off the FTP engine's use of the EPSV command. By
        default, that one will always be attempted before the more traditional
        PASV command. */
    CURLOPT_FTP_USE_EPSV = CURLOPTTYPE.LONG + 85,

    /* type of the file keeping your SSL-certificate ("DER", "PEM", "ENG") */
    CURLOPT_SSLCERTTYPE = CURLOPTTYPE.STRINGPOINT + 86,

    /* name of the file keeping your private SSL-key */
    CURLOPT_SSLKEY = CURLOPTTYPE.STRINGPOINT + 87,

    /* type of the file keeping your private SSL-key ("DER", "PEM", "ENG") */
    CURLOPT_SSLKEYTYPE = CURLOPTTYPE.STRINGPOINT + 88,

    /* crypto engine for the SSL-sub system */
    CURLOPT_SSLENGINE = CURLOPTTYPE.STRINGPOINT + 89,

    /* set the crypto engine for the SSL-sub system as default
        the param has no meaning...
     */
    CURLOPT_SSLENGINE_DEFAULT = CURLOPTTYPE.LONG + 90,

    /* Non-zero value means to use the global dns cache */
    /* DEPRECATED, do not use! */
    CURLOPT_DNS_USE_GLOBAL_CACHE = CURLOPTTYPE.LONG + 91,

    /* DNS cache timeout */
    CURLOPT_DNS_CACHE_TIMEOUT = CURLOPTTYPE.LONG + 92,

    /* send linked-list of pre-transfer QUOTE commands */
    CURLOPT_PREQUOTE = CURLOPTTYPE.SLISTPOINT + 93,

    /* set the debug function */
    CURLOPT_DEBUGFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 94,

    /* set the data for the debug function */
    CURLOPT_DEBUGDATA = CURLOPTTYPE.CBPOINT + 95,

    /* mark this as start of a cookie session */
    CURLOPT_COOKIESESSION = CURLOPTTYPE.LONG + 96,

    /* The CApath directory used to validate the peer certificate
        this option is used only if SSL_VERIFYPEER is true */
    CURLOPT_CAPATH = CURLOPTTYPE.STRINGPOINT + 97,

    /* Instruct libcurl to use a smaller receive buffer */
    CURLOPT_BUFFERSIZE = CURLOPTTYPE.LONG + 98,

    /* Instruct libcurl to not use any signal/alarm easyrs, even when using
        timeouts. This option is useful for multi-threaded applications.
        See libcurl-the-guide for more background information. */
    CURLOPT_NOSIGNAL = CURLOPTTYPE.LONG + 99,

    /* Provide a CURLShare for mutexing non-ts data */
    CURLOPT_SHARE = CURLOPTTYPE.OBJECTPOINT + 100,

    /* indicates type of proxy. accepted values are CURLPROXY_HTTP (default),
        CURLPROXY_HTTPS, CURLPROXY_SOCKS4, CURLPROXY_SOCKS4A and
        CURLPROXY_SOCKS5. */
    CURLOPT_PROXYTYPE = CURLOPTTYPE.VALUES + 101,

    /* Set the Accept-Encoding string. Use this to tell a server you would like
        the response to be compressed. Before 7.21.6, this was known as
        CURLOPT_ENCODING */
    CURLOPT_ACCEPT_ENCODING = CURLOPTTYPE.STRINGPOINT + 102,

    /* Set pointer to private data */
    CURLOPT_PRIVATE = CURLOPTTYPE.OBJECTPOINT + 103,

    /* Set aliases for HTTP 200 in the HTTP Response header */
    CURLOPT_HTTP200ALIASES = CURLOPTTYPE.SLISTPOINT + 104,

    /* Continue to send authentication (user+password) when following locations,
        even when hostname changed. This can potentially send off the name
        and password to whatever host the server decides. */
    CURLOPT_UNRESTRICTED_AUTH = CURLOPTTYPE.LONG + 105,

    /* Specifically switch on or off the FTP engine's use of the EPRT command (
        it also disables the LPRT attempt). By default, those ones will always be
        attempted before the good old traditional PORT command. */
    CURLOPT_FTP_USE_EPRT = CURLOPTTYPE.LONG + 106,

    /* Set this to a bitmask value to enable the particular authentications
        methods you like. Use this in combination with CURLOPT_USERPWD.
        Note that setting multiple bits may cause extra network round-trips. */
    CURLOPT_HTTPAUTH = CURLOPTTYPE.VALUES + 107,

    /* Set the ssl context callback function, currently only for OpenSSL or
        WolfSSL ssl_ctx, or mbedTLS mbedtls_ssl_config in the second argument.
        The function must match the curl_ssl_ctx_callback prototype. */
    CURLOPT_SSL_CTX_FUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 108,

    /* Set the userdata for the ssl context callback function's third
        argument */
    CURLOPT_SSL_CTX_DATA = CURLOPTTYPE.CBPOINT + 109,

    /* FTP Option that causes missing dirs to be created on the remote server.
        In 7.19.4 we introduced the convenience enums for this option using the
        CURLFTP_CREATE_DIR prefix.
    */
    CURLOPT_FTP_CREATE_MISSING_DIRS = CURLOPTTYPE.LONG + 110,

    /* Set this to a bitmask value to enable the particular authentications
        methods you like. Use this in combination with CURLOPT_PROXYUSERPWD.
        Note that setting multiple bits may cause extra network round-trips. */
    CURLOPT_PROXYAUTH = CURLOPTTYPE.VALUES + 111,

    /* FTP option that changes the timeout, in seconds, associated with
        getting a response.  This is different from transfer timeout time and
        essentially places a demand on the FTP server to acknowledge commands
        in a timely manner. */
    CURLOPT_FTP_RESPONSE_TIMEOUT = CURLOPTTYPE.LONG + 112,
    /*#define CURLOPT_SERVER_RESPONSE_TIMEOUT CURLOPT_FTP_RESPONSE_TIMEOUT*/

    /* Set this option to one of the CURL_IPRESOLVE_* defines (see below) to
        tell libcurl to use those IP versions only. This only has effect on
        systems with support for more than one, i.e IPv4 _and_ IPv6. */
    CURLOPT_IPRESOLVE = CURLOPTTYPE.VALUES + 113,

    /* Set this option to limit the size of a file that will be downloaded from
        an HTTP or FTP server.
        Note there is also _LARGE version which adds large file support for
        platforms which have larger off_t sizes.  See MAXFILESIZE_LARGE below. */
    CURLOPT_MAXFILESIZE = CURLOPTTYPE.LONG + 114,

    /* See the comment for INFILESIZE above, but in short, specifies
     * the size of the file being uploaded.  -1 means unknown.
     */
    CURLOPT_INFILESIZE_LARGE = CURLOPTTYPE.OFF_T + 115,

    /* Sets the continuation offset.  There is also a CURLOPTTYPE.LONG version
     * of this; look above for RESUME_FROM.
     */
    CURLOPT_RESUME_FROM_LARGE = CURLOPTTYPE.OFF_T + 116,

    /* Sets the maximum size of data that will be downloaded from
     * an HTTP or FTP server.  See MAXFILESIZE above for the LONG version.
     */
    CURLOPT_MAXFILESIZE_LARGE = CURLOPTTYPE.OFF_T + 117,

    /* Set this option to the file name of your .netrc file you want libcurl
        to parse (using the CURLOPT_NETRC option). If not set, libcurl will do
        a poor attempt to find the user's home directory and check for a .netrc
        file in there. */
    CURLOPT_NETRC_FILE = CURLOPTTYPE.STRINGPOINT + 118,

    /* Enable SSL/TLS for FTP, pick one of:
        CURLUSESSL_TRY     - try using SSL, proceed anyway otherwise
        CURLUSESSL_CONTROL - SSL for the control connection or fail
        CURLUSESSL_ALL     - SSL for all communication or fail
    */
    CURLOPT_USE_SSL = CURLOPTTYPE.VALUES + 119,

    /* The _LARGE version of the standard POSTFIELDSIZE option */
    CURLOPT_POSTFIELDSIZE_LARGE = CURLOPTTYPE.OFF_T + 120,

    /* Enable/disable the TCP Nagle algorithm */
    CURLOPT_TCP_NODELAY = CURLOPTTYPE.LONG + 121,

    /* 122 OBSOLETE, used in 7.12.3. Gone in 7.13.0 */
    /* 123 OBSOLETE. Gone in 7.16.0 */
    /* 124 OBSOLETE, used in 7.12.3. Gone in 7.13.0 */
    /* 125 OBSOLETE, used in 7.12.3. Gone in 7.13.0 */
    /* 126 OBSOLETE, used in 7.12.3. Gone in 7.13.0 */
    /* 127 OBSOLETE. Gone in 7.16.0 */
    /* 128 OBSOLETE. Gone in 7.16.0 */

    /* When FTP over SSL/TLS is selected (with CURLOPT_USE_SSL), this option
        can be used to change libcurl's default action which is to first try
        "AUTH SSL" and then "AUTH TLS" in this order, and proceed when a OK
        response has been received.
        Available parameters are:
        CURLFTPAUTH_DEFAULT - let libcurl decide
        CURLFTPAUTH_SSL     - try "AUTH SSL" first, then TLS
        CURLFTPAUTH_TLS     - try "AUTH TLS" first, then SSL
    */
    CURLOPT_FTPSSLAUTH = CURLOPTTYPE.VALUES + 129,

    CURLOPT_IOCTLFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 130,
    CURLOPT_IOCTLDATA = CURLOPTTYPE.CBPOINT + 131,

    /* 132 OBSOLETE. Gone in 7.16.0 */
    /* 133 OBSOLETE. Gone in 7.16.0 */

    /* null-terminated string for pass on to the FTP server when asked for
        "account" info */
    CURLOPT_FTP_ACCOUNT = CURLOPTTYPE.STRINGPOINT + 134,

    /* feed cookie into cookie engine */
    CURLOPT_COOKIELIST = CURLOPTTYPE.STRINGPOINT + 135,

    /* ignore Content-Length */
    CURLOPT_IGNORE_CONTENT_LENGTH = CURLOPTTYPE.LONG + 136,

    /* Set to non-zero to skip the IP address received in a 227 PASV FTP server
        response. Typically used for FTP-SSL purposes but is not restricted to
        that. libcurl will then instead use the same IP address it used for the
        control connection. */
    CURLOPT_FTP_SKIP_PASV_IP = CURLOPTTYPE.LONG + 137,

    /* Select "file method" to use when doing FTP, see the curl_ftpmethod
        above. */
    CURLOPT_FTP_FILEMETHOD = CURLOPTTYPE.VALUES + 138,

    /* Local port number to bind the socket to */
    CURLOPT_LOCALPORT = CURLOPTTYPE.LONG + 139,

    /* Number of ports to try, including the first one set with LOCALPORT.
        Thus, setting it to 1 will make no additional attempts but the first.
    */
    CURLOPT_LOCALPORTRANGE = CURLOPTTYPE.LONG + 140,

    /* no transfer, set up connection and let application use the socket by
        extracting it with CURLINFO_LASTSOCKET */
    CURLOPT_CONNECT_ONLY = CURLOPTTYPE.LONG + 141,

    /* Function that will be called to convert from the
        network encoding (instead of using the iconv calls in libcurl) */
    CURLOPT_CONV_FROM_NETWORK_FUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 142,

    /* Function that will be called to convert to the
        network encoding (instead of using the iconv calls in libcurl) */
    CURLOPT_CONV_TO_NETWORK_FUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 143,

    /* Function that will be called to convert from UTF8
        (instead of using the iconv calls in libcurl)
        Note that this is used only for SSL certificate processing */
    CURLOPT_CONV_FROM_UTF8_FUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 144,

    /* if the connection proceeds too quickly then need to slow it down */
    /* limit-rate: maximum number of bytes per second to send or receive */
    CURLOPT_MAX_SEND_SPEED_LARGE = CURLOPTTYPE.OFF_T + 145,
    CURLOPT_MAX_RECV_SPEED_LARGE = CURLOPTTYPE.OFF_T + 146,

    /* Pointer to command string to send if USER/PASS fails. */
    CURLOPT_FTP_ALTERNATIVE_TO_USER = CURLOPTTYPE.STRINGPOINT + 147,

    /* callback function for setting socket options */
    CURLOPT_SOCKOPTFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 148,
    CURLOPT_SOCKOPTDATA = CURLOPTTYPE.CBPOINT + 149,

    /* set to 0 to disable session ID re-use for this transfer, default is
        enabled (== 1) */
    CURLOPT_SSL_SESSIONID_CACHE = CURLOPTTYPE.LONG + 150,

    /* allowed SSH authentication methods */
    CURLOPT_SSH_AUTH_TYPES = CURLOPTTYPE.VALUES + 151,

    /* Used by scp/sftp to do public/private key authentication */
    CURLOPT_SSH_PUBLIC_KEYFILE = CURLOPTTYPE.STRINGPOINT + 152,
    CURLOPT_SSH_PRIVATE_KEYFILE = CURLOPTTYPE.STRINGPOINT + 153,

    /* Send CCC (Clear Command Channel) after authentication */
    CURLOPT_FTP_SSL_CCC = CURLOPTTYPE.LONG + 154,

    /* Same as TIMEOUT and CONNECTTIMEOUT, but with ms resolution */
    CURLOPT_TIMEOUT_MS = CURLOPTTYPE.LONG + 155,
    CURLOPT_CONNECTTIMEOUT_MS = CURLOPTTYPE.LONG + 156,

    /* set to zero to disable the libcurl's decoding and thus pass the raw body
        data to the application even when it is encoded/compressed */
    CURLOPT_HTTP_TRANSFER_DECODING = CURLOPTTYPE.LONG + 157,
    CURLOPT_HTTP_CONTENT_DECODING = CURLOPTTYPE.LONG + 158,

    /* Permission used when creating new files and directories on the remote
        server for protocols that support it, SFTP/SCP/FILE */
    CURLOPT_NEW_FILE_PERMS = CURLOPTTYPE.LONG + 159,
    CURLOPT_NEW_DIRECTORY_PERMS = CURLOPTTYPE.LONG + 160,

    /* Set the behavior of POST when redirecting. Values must be set to one
        of CURL_REDIR* defines below. This used to be called CURLOPT_POST301 */
    CURLOPT_POSTREDIR = CURLOPTTYPE.VALUES + 161,

    /* used by scp/sftp to verify the host's public key */
    CURLOPT_SSH_HOST_PUBLIC_KEY_MD5 = CURLOPTTYPE.STRINGPOINT + 162,

    /* Callback function for opening socket (instead of socket(2)). Optionally,
        callback is able change the address or refuse to connect returning
        CURL_SOCKET_BAD.  The callback should have type
        curl_opensocket_callback */
    CURLOPT_OPENSOCKETFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 163,
    CURLOPT_OPENSOCKETDATA = CURLOPTTYPE.CBPOINT + 164,

    /* POST volatile input fields. */
    CURLOPT_COPYPOSTFIELDS = CURLOPTTYPE.OBJECTPOINT + 165,

    /* set transfer mode (;type=<a|i>) when doing FTP via an HTTP proxy */
    CURLOPT_PROXY_TRANSFER_MODE = CURLOPTTYPE.LONG + 166,

    /* Callback function for seeking in the input stream */
    CURLOPT_SEEKFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 167,
    CURLOPT_SEEKDATA = CURLOPTTYPE.CBPOINT + 168,

    /* CRL file */
    CURLOPT_CRLFILE = CURLOPTTYPE.STRINGPOINT + 169,

    /* Issuer certificate */
    CURLOPT_ISSUERCERT = CURLOPTTYPE.STRINGPOINT + 170,

    /* (IPv6) Address scope */
    CURLOPT_ADDRESS_SCOPE = CURLOPTTYPE.LONG + 171,

    /* Collect certificate chain info and allow it to get retrievable with
        CURLINFO_CERTINFO after the transfer is complete. */
    CURLOPT_CERTINFO = CURLOPTTYPE.LONG + 172,

    /* "name" and "pwd" to use when fetching. */
    CURLOPT_USERNAME = CURLOPTTYPE.STRINGPOINT + 173,
    CURLOPT_PASSWORD = CURLOPTTYPE.STRINGPOINT + 174,

    /* "name" and "pwd" to use with Proxy when fetching. */
    CURLOPT_PROXYUSERNAME = CURLOPTTYPE.STRINGPOINT + 175,
    CURLOPT_PROXYPASSWORD = CURLOPTTYPE.STRINGPOINT + 176,

    /* Comma separated list of hostnames defining no-proxy zones. These should
        match both hostnames directly, and hostnames within a domain. For
        example, local.com will match local.com and www.local.com, but NOT
        notlocal.com or www.notlocal.com. For compatibility with other
        implementations of this, .local.com will be considered to be the same as
        local.com. A single * is the only valid wildcard, and effectively
        disables the use of proxy. */
    CURLOPT_NOPROXY = CURLOPTTYPE.STRINGPOINT + 177,

    /* block size for TFTP transfers */
    CURLOPT_TFTP_BLKSIZE = CURLOPTTYPE.LONG + 178,

    /* Socks Service */
    /* DEPRECATED, do not use! */
    CURLOPT_SOCKS5_GSSAPI_SERVICE = CURLOPTTYPE.STRINGPOINT + 179,

    /* Socks Service */
    CURLOPT_SOCKS5_GSSAPI_NEC = CURLOPTTYPE.LONG + 180,

    /* set the bitmask for the protocols that are allowed to be used for the
        transfer, which thus helps the app which takes URLs from users or other
        external inputs and want to restrict what protocol(s) to deal
        with. Defaults to CURLPROTO_ALL. */
    CURLOPT_PROTOCOLS = CURLOPTTYPE.LONG + 181,

    /* set the bitmask for the protocols that libcurl is allowed to follow to,
        as a subset of the CURLOPT_PROTOCOLS ones. That means the protocol needs
        to be set in both bitmasks to be allowed to get redirected to. */
    CURLOPT_REDIR_PROTOCOLS = CURLOPTTYPE.LONG + 182,

    /* set the SSH knownhost file name to use */
    CURLOPT_SSH_KNOWNHOSTS = CURLOPTTYPE.STRINGPOINT + 183,

    /* set the SSH host key callback, must point to a curl_sshkeycallback
        function */
    CURLOPT_SSH_KEYFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 184,

    /* set the SSH host key callback custom pointer */
    CURLOPT_SSH_KEYDATA = CURLOPTTYPE.CBPOINT + 185,

    /* set the SMTP mail originator */
    CURLOPT_MAIL_FROM = CURLOPTTYPE.STRINGPOINT + 186,

    /* set the list of SMTP mail receiver(s) */
    CURLOPT_MAIL_RCPT = CURLOPTTYPE.SLISTPOINT + 187,

    /* FTP: send PRET before PASV */
    CURLOPT_FTP_USE_PRET = CURLOPTTYPE.LONG + 188,

    /* RTSP request method (OPTIONS, SETUP, PLAY, etc...) */
    CURLOPT_RTSP_REQUEST = CURLOPTTYPE.VALUES + 189,

    /* The RTSP session identifier */
    CURLOPT_RTSP_SESSION_ID = CURLOPTTYPE.STRINGPOINT + 190,

    /* The RTSP stream URI */
    CURLOPT_RTSP_STREAM_URI = CURLOPTTYPE.STRINGPOINT + 191,

    /* The Transport: header to use in RTSP requests */
    CURLOPT_RTSP_TRANSPORT = CURLOPTTYPE.STRINGPOINT + 192,

    /* Manually initialize the client RTSP CSeq for this easy */
    CURLOPT_RTSP_CLIENT_CSEQ = CURLOPTTYPE.LONG + 193,

    /* Manually initialize the server RTSP CSeq for this easy */
    CURLOPT_RTSP_SERVER_CSEQ = CURLOPTTYPE.LONG + 194,

    /* The stream to pass to INTERLEAVEFUNCTION. */
    CURLOPT_INTERLEAVEDATA = CURLOPTTYPE.CBPOINT + 195,

    /* Let the application define a custom write method for RTP data */
    CURLOPT_INTERLEAVEFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 196,

    /* Turn on wildcard matching */
    CURLOPT_WILDCARDMATCH = CURLOPTTYPE.LONG + 197,

    /* Directory matching callback called before downloading of an
        individual file (chunk) started */
    CURLOPT_CHUNK_BGN_FUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 198,

    /* Directory matching callback called after the file (chunk)
        was downloaded, or skipped */
    CURLOPT_CHUNK_END_FUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 199,

    /* Change match (fnmatch-like) callback for wildcard matching */
    CURLOPT_FNMATCH_FUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 200,

    /* Let the application define custom chunk data pointer */
    CURLOPT_CHUNK_DATA = CURLOPTTYPE.CBPOINT + 201,

    /* FNMATCH_FUNCTION user pointer */
    CURLOPT_FNMATCH_DATA = CURLOPTTYPE.CBPOINT + 202,

    /* send linked-list of name:port:address sets */
    CURLOPT_RESOLVE = CURLOPTTYPE.SLISTPOINT + 203,

    /* Set a username for authenticated TLS */
    CURLOPT_TLSAUTH_USERNAME = CURLOPTTYPE.STRINGPOINT + 204,

    /* Set a password for authenticated TLS */
    CURLOPT_TLSAUTH_PASSWORD = CURLOPTTYPE.STRINGPOINT + 205,

    /* Set authentication type for authenticated TLS */
    CURLOPT_TLSAUTH_TYPE = CURLOPTTYPE.STRINGPOINT + 206,

    /* Set to 1 to enable the "TE:" header in HTTP requests to ask for
        compressed transfer-encoded responses. Set to 0 to disable the use of TE:
        in outgoing requests. The current default is 0, but it might change in a
        future libcurl release.
        libcurl will ask for the compressed methods it knows of, and if that
        isn't any, it will not ask for transfer-encoding at all even if this
        option is set to 1.
    */
    CURLOPT_TRANSFER_ENCODING = CURLOPTTYPE.LONG + 207,

    /* Callback function for closing socket (instead of close(2)). The callback
        should have type curl_closesocket_callback */
    CURLOPT_CLOSESOCKETFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 208,
    CURLOPT_CLOSESOCKETDATA = CURLOPTTYPE.CBPOINT + 209,

    /* allow GSSAPI credential delegation */
    CURLOPT_GSSAPI_DELEGATION = CURLOPTTYPE.VALUES + 210,

    /* Set the name servers to use for DNS resolution */
    CURLOPT_DNS_SERVERS = CURLOPTTYPE.STRINGPOINT + 211,

    /* Time-out accept operations (currently for FTP only) after this amount
        of milliseconds. */
    CURLOPT_ACCEPTTIMEOUT_MS = CURLOPTTYPE.LONG + 212,

    /* Set TCP keepalive */
    CURLOPT_TCP_KEEPALIVE = CURLOPTTYPE.LONG + 213,

    /* non-universal keepalive knobs (Linux, AIX, HP-UX, more) */
    CURLOPT_TCP_KEEPIDLE = CURLOPTTYPE.LONG + 214,
    CURLOPT_TCP_KEEPINTVL = CURLOPTTYPE.LONG + 215,

    /* Enable/disable specific SSL features with a bitmask, see CURLSSLOPT_* */
    CURLOPT_SSL_OPTIONS = CURLOPTTYPE.VALUES + 216,

    /* Set the SMTP auth originator */
    CURLOPT_MAIL_AUTH = CURLOPTTYPE.STRINGPOINT + 217,

    /* Enable/disable SASL initial response */
    CURLOPT_SASL_IR = CURLOPTTYPE.LONG + 218,

    /* Function that will be called instead of the internal progress display
     * function. This function should be defined as the curl_xferinfo_callback
     * prototype defines. (Deprecates CURLOPT_PROGRESSFUNCTION) */
    CURLOPT_XFERINFOFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 219,

    /* The XOAUTH2 bearer token */
    CURLOPT_XOAUTH2_BEARER = CURLOPTTYPE.STRINGPOINT + 220,

    /* Set the interface string to use as outgoing network
     * interface for DNS requests.
     * Only supported by the c-ares DNS backend */
    CURLOPT_DNS_INTERFACE = CURLOPTTYPE.STRINGPOINT + 221,

    /* Set the local IPv4 address to use for outgoing DNS requests.
     * Only supported by the c-ares DNS backend */
    CURLOPT_DNS_LOCAL_IP4 = CURLOPTTYPE.STRINGPOINT + 222,

    /* Set the local IPv6 address to use for outgoing DNS requests.
     * Only supported by the c-ares DNS backend */
    CURLOPT_DNS_LOCAL_IP6 = CURLOPTTYPE.STRINGPOINT + 223,

    /* Set authentication options directly */
    CURLOPT_LOGIN_OPTIONS = CURLOPTTYPE.STRINGPOINT + 224,

    /* Enable/disable TLS NPN extension (http2 over ssl might fail without) */
    CURLOPT_SSL_ENABLE_NPN = CURLOPTTYPE.LONG + 225,

    /* Enable/disable TLS ALPN extension (http2 over ssl might fail without) */
    CURLOPT_SSL_ENABLE_ALPN = CURLOPTTYPE.LONG + 226,

    /* Time to wait for a response to a HTTP request containing an
     * Expect: 100-continue header before sending the data anyway. */
    CURLOPT_EXPECT_100_TIMEOUT_MS = CURLOPTTYPE.LONG + 227,

    /* This points to a linked list of headers used for proxy requests only,
        struct curl_slist kind */
    CURLOPT_PROXYHEADER = CURLOPTTYPE.SLISTPOINT + 228,

    /* Pass in a bitmask of "header options" */
    CURLOPT_HEADEROPT = CURLOPTTYPE.VALUES + 229,

    /* The public key in DER form used to validate the peer public key
        this option is used only if SSL_VERIFYPEER is true */
    CURLOPT_PINNEDPUBLICKEY = CURLOPTTYPE.STRINGPOINT + 230,

    /* Path to Unix domain socket */
    CURLOPT_UNIX_SOCKET_PATH = CURLOPTTYPE.STRINGPOINT + 231,

    /* Set if we should verify the certificate status. */
    CURLOPT_SSL_VERIFYSTATUS = CURLOPTTYPE.LONG + 232,

    /* Set if we should enable TLS false start. */
    CURLOPT_SSL_FALSESTART = CURLOPTTYPE.LONG + 233,

    /* Do not squash dot-dot sequences */
    CURLOPT_PATH_AS_IS = CURLOPTTYPE.LONG + 234,

    /* Proxy Service Name */
    CURLOPT_PROXY_SERVICE_NAME = CURLOPTTYPE.STRINGPOINT + 235,

    /* Service Name */
    CURLOPT_SERVICE_NAME = CURLOPTTYPE.STRINGPOINT + 236,

    /* Wait/don't wait for pipe/mutex to clarify */
    CURLOPT_PIPEWAIT = CURLOPTTYPE.LONG + 237,

    /* Set the protocol used when curl is given a URL without a protocol */
    CURLOPT_DEFAULT_PROTOCOL = CURLOPTTYPE.STRINGPOINT + 238,

    /* Set stream weight, 1 - 256 (default is 16) */
    CURLOPT_STREAM_WEIGHT = CURLOPTTYPE.LONG + 239,

    /* Set stream dependency on another CURL easy */
    CURLOPT_STREAM_DEPENDS = CURLOPTTYPE.OBJECTPOINT + 240,

    /* Set E-xclusive stream dependency on another CURL easy */
    CURLOPT_STREAM_DEPENDS_E = CURLOPTTYPE.OBJECTPOINT + 241,

    /* Do not send any tftp option requests to the server */
    CURLOPT_TFTP_NO_OPTIONS = CURLOPTTYPE.LONG + 242,

    /* Linked-list of host:port:connect-to-host:connect-to-port,
        overrides the URL's host:port (only for the network layer) */
    CURLOPT_CONNECT_TO = CURLOPTTYPE.SLISTPOINT + 243,

    /* Set TCP Fast Open */
    CURLOPT_TCP_FASTOPEN = CURLOPTTYPE.LONG + 244,

    /* Continue to send data if the server responds early with an
     * HTTP status code >= 300 */
    CURLOPT_KEEP_SENDING_ON_ERROR = CURLOPTTYPE.LONG + 245,

    /* The CApath or CAfile used to validate the proxy certificate
        this option is used only if PROXY_SSL_VERIFYPEER is true */
    CURLOPT_PROXY_CAINFO = CURLOPTTYPE.STRINGPOINT + 246,

    /* The CApath directory used to validate the proxy certificate
        this option is used only if PROXY_SSL_VERIFYPEER is true */
    CURLOPT_PROXY_CAPATH = CURLOPTTYPE.STRINGPOINT + 247,

    /* Set if we should verify the proxy in ssl handshake,
        set 1 to verify. */
    CURLOPT_PROXY_SSL_VERIFYPEER = CURLOPTTYPE.LONG + 248,

    /* Set if we should verify the Common name from the proxy certificate in ssl
     * handshake, set 1 to check existence, 2 to ensure that it matches
     * the provided hostname. */
    CURLOPT_PROXY_SSL_VERIFYHOST = CURLOPTTYPE.LONG + 249,

    /* What version to specifically try to use for proxy.
        See CURL_SSLVERSION defines below. */
    CURLOPT_PROXY_SSLVERSION = CURLOPTTYPE.VALUES + 250,

    /* Set a username for authenticated TLS for proxy */
    CURLOPT_PROXY_TLSAUTH_USERNAME = CURLOPTTYPE.STRINGPOINT + 251,

    /* Set a password for authenticated TLS for proxy */
    CURLOPT_PROXY_TLSAUTH_PASSWORD = CURLOPTTYPE.STRINGPOINT + 252,

    /* Set authentication type for authenticated TLS for proxy */
    CURLOPT_PROXY_TLSAUTH_TYPE = CURLOPTTYPE.STRINGPOINT + 253,

    /* name of the file keeping your private SSL-certificate for proxy */
    CURLOPT_PROXY_SSLCERT = CURLOPTTYPE.STRINGPOINT + 254,

    /* type of the file keeping your SSL-certificate ("DER", "PEM", "ENG") for
        proxy */
    CURLOPT_PROXY_SSLCERTTYPE = CURLOPTTYPE.STRINGPOINT + 255,

    /* name of the file keeping your private SSL-key for proxy */
    CURLOPT_PROXY_SSLKEY = CURLOPTTYPE.STRINGPOINT + 256,

    /* type of the file keeping your private SSL-key ("DER", "PEM", "ENG") for
        proxy */
    CURLOPT_PROXY_SSLKEYTYPE = CURLOPTTYPE.STRINGPOINT + 257,

    /* password for the SSL private key for proxy */
    CURLOPT_PROXY_KEYPASSWD = CURLOPTTYPE.STRINGPOINT + 258,

    /* Specify which SSL ciphers to use for proxy */
    CURLOPT_PROXY_SSL_CIPHER_LIST = CURLOPTTYPE.STRINGPOINT + 259,

    /* CRL file for proxy */
    CURLOPT_PROXY_CRLFILE = CURLOPTTYPE.STRINGPOINT + 260,

    /* Enable/disable specific SSL features with a bitmask for proxy, see
        CURLSSLOPT_* */
    CURLOPT_PROXY_SSL_OPTIONS = CURLOPTTYPE.LONG + 261,

    /* Name of pre proxy to use. */
    CURLOPT_PRE_PROXY = CURLOPTTYPE.STRINGPOINT + 262,

    /* The public key in DER form used to validate the proxy public key
        this option is used only if PROXY_SSL_VERIFYPEER is true */
    CURLOPT_PROXY_PINNEDPUBLICKEY = CURLOPTTYPE.STRINGPOINT + 263,

    /* Path to an abstract Unix domain socket */
    CURLOPT_ABSTRACT_UNIX_SOCKET = CURLOPTTYPE.STRINGPOINT + 264,

    /* Suppress proxy CONNECT response headers from user callbacks */
    CURLOPT_SUPPRESS_CONNECT_HEADERS = CURLOPTTYPE.LONG + 265,

    /* The request target, instead of extracted from the URL */
    CURLOPT_REQUEST_TARGET = CURLOPTTYPE.STRINGPOINT + 266,

    /* bitmask of allowed auth methods for connections to SOCKS5 proxies */
    CURLOPT_SOCKS5_AUTH = CURLOPTTYPE.LONG + 267,

    /* Enable/disable SSH compression */
    CURLOPT_SSH_COMPRESSION = CURLOPTTYPE.LONG + 268,

    /* Post MIME data. */
    CURLOPT_MIMEPOST = CURLOPTTYPE.OBJECTPOINT + 269,

    /* Time to use with the CURLOPT_TIMECONDITION. Specified in number of
        seconds since 1 Jan 1970. */
    CURLOPT_TIMEVALUE_LARGE = CURLOPTTYPE.OFF_T + 270,

    /* Head start in milliseconds to give happy eyeballs. */
    CURLOPT_HAPPY_EYEBALLS_TIMEOUT_MS = CURLOPTTYPE.LONG + 271,

    /* Function that will be called before a resolver request is made */
    CURLOPT_RESOLVER_START_FUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 272,

    /* User data to pass to the resolver start callback. */
    CURLOPT_RESOLVER_START_DATA = CURLOPTTYPE.CBPOINT + 273,

    /* send HAProxy PROXY protocol header? */
    CURLOPT_HAPROXYPROTOCOL = CURLOPTTYPE.LONG + 274,

    /* shuffle addresses before use when DNS returns multiple */
    CURLOPT_DNS_SHUFFLE_ADDRESSES = CURLOPTTYPE.LONG + 275,

    /* Specify which TLS 1.3 ciphers suites to use */
    CURLOPT_TLS13_CIPHERS = CURLOPTTYPE.STRINGPOINT + 276,
    CURLOPT_PROXY_TLS13_CIPHERS = CURLOPTTYPE.STRINGPOINT + 277,

    /* Disallow specifying username/login in URL. */
    CURLOPT_DISALLOW_USERNAME_IN_URL = CURLOPTTYPE.LONG + 278,

    /* DNS-over-HTTPS URL */
    CURLOPT_DOH_URL = CURLOPTTYPE.STRINGPOINT + 279,

    /* Preferred buffer size to use for uploads */
    CURLOPT_UPLOAD_BUFFERSIZE = CURLOPTTYPE.LONG + 280,

    /* Time in ms between connection upkeep calls for long-lived connections. */
    CURLOPT_UPKEEP_INTERVAL_MS = CURLOPTTYPE.LONG + 281,

    /* Specify URL using CURL URL API. */
    CURLOPT_CURLU = CURLOPTTYPE.OBJECTPOINT + 282,

    /* add trailing data just after no more data is available */
    CURLOPT_TRAILERFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 283,

    /* pointer to be passed to HTTP_TRAILER_FUNCTION */
    CURLOPT_TRAILERDATA = CURLOPTTYPE.CBPOINT + 284,

    /* set this to 1L to allow HTTP/0.9 responses or 0L to disallow */
    CURLOPT_HTTP09_ALLOWED = CURLOPTTYPE.LONG + 285,

    /* alt-svc control bitmask */
    CURLOPT_ALTSVC_CTRL = CURLOPTTYPE.LONG + 286,

    /* alt-svc cache file name to possibly read from/write to */
    CURLOPT_ALTSVC = CURLOPTTYPE.STRINGPOINT + 287,

    /* maximum age (idle time) of a connection to consider it for reuse
     * (in seconds) */
    CURLOPT_MAXAGE_CONN = CURLOPTTYPE.LONG + 288,

    /* SASL authorisation identity */
    CURLOPT_SASL_AUTHZID = CURLOPTTYPE.STRINGPOINT + 289,

    /* allow RCPT TO command to fail for some recipients */
    CURLOPT_MAIL_RCPT_ALLLOWFAILS = CURLOPTTYPE.LONG + 290,

    /* the private SSL-certificate as a "blob" */
    CURLOPT_SSLCERT_BLOB = CURLOPTTYPE.BLOB + 291,
    CURLOPT_SSLKEY_BLOB = CURLOPTTYPE.BLOB + 292,
    CURLOPT_PROXY_SSLCERT_BLOB = CURLOPTTYPE.BLOB + 293,
    CURLOPT_PROXY_SSLKEY_BLOB = CURLOPTTYPE.BLOB + 294,
    CURLOPT_ISSUERCERT_BLOB = CURLOPTTYPE.BLOB + 295,

    /* Issuer certificate for proxy */
    CURLOPT_PROXY_ISSUERCERT = CURLOPTTYPE.STRINGPOINT + 296,
    CURLOPT_PROXY_ISSUERCERT_BLOB = CURLOPTTYPE.BLOB + 297,

    /* the EC curves requested by the TLS client (RFC 8422, 5.1);
     * OpenSSL support via 'set_groups'/'set_curves':
     * https://www.openssl.org/docs/manmaster/man3/SSL_CTX_set1_groups.html
     */
    CURLOPT_SSL_EC_CURVES = CURLOPTTYPE.STRINGPOINT + 298,

    /* HSTS bitmask */
    CURLOPT_HSTS_CTRL = CURLOPTTYPE.LONG + 299,
    /* HSTS file name */
    CURLOPT_HSTS = CURLOPTTYPE.STRINGPOINT + 300,

    /* HSTS read callback */
    CURLOPT_HSTSREADFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 301,
    CURLOPT_HSTSREADDATA = CURLOPTTYPE.CBPOINT + 302,

    /* HSTS write callback */
    CURLOPT_HSTSWRITEFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 303,
    CURLOPT_HSTSWRITEDATA = CURLOPTTYPE.CBPOINT + 304,

    /* Parameters for V4 signature */
    CURLOPT_AWS_SIGV4 = CURLOPTTYPE.STRINGPOINT + 305,

    /* Same as CURLOPT_SSL_VERIFYPEER but for DoH (DNS-over-HTTPS) servers. */
    CURLOPT_DOH_SSL_VERIFYPEER = CURLOPTTYPE.LONG + 306,

    /* Same as CURLOPT_SSL_VERIFYHOST but for DoH (DNS-over-HTTPS) servers. */
    CURLOPT_DOH_SSL_VERIFYHOST = CURLOPTTYPE.LONG + 307,

    /* Same as CURLOPT_SSL_VERIFYSTATUS but for DoH (DNS-over-HTTPS) servers. */
    CURLOPT_DOH_SSL_VERIFYSTATUS = CURLOPTTYPE.LONG + 308,

    /* The CA certificates as "blob" used to validate the peer certificate
        this option is used only if SSL_VERIFYPEER is true */
    CURLOPT_CAINFO_BLOB = CURLOPTTYPE.BLOB + 309,

    /* The CA certificates as "blob" used to validate the proxy certificate
        this option is used only if PROXY_SSL_VERIFYPEER is true */
    CURLOPT_PROXY_CAINFO_BLOB = CURLOPTTYPE.BLOB + 310,

    /* used by scp/sftp to verify the host's public key */
    CURLOPT_SSH_HOST_PUBLIC_KEY_SHA256 = CURLOPTTYPE.STRINGPOINT + 311,

    /* Function that will be called immediately before the initial request
        is made on a connection (after any protocol negotiation step).  */
    CURLOPT_PREREQFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 312,

    /* Data passed to the CURLOPT_PREREQFUNCTION callback */
    CURLOPT_PREREQDATA = CURLOPTTYPE.CBPOINT + 313,

    /* maximum age (since creation) of a connection to consider it for reuse
     * (in seconds) */
    CURLOPT_MAXLIFETIME_CONN = CURLOPTTYPE.LONG + 314,

    /* Set MIME option flags. */
    CURLOPT_MIME_OPTIONS = CURLOPTTYPE.LONG + 315,

    CURLOPT_LASTENTRY /* the last unused */
}

public enum CURLMcode
{
    CURLM_CALL_MULTI_PERFORM = -1, /* please call curl_multi_perform() or
                                    curl_multi_socket*() soon */
    CURLM_OK,
    CURLM_BAD_HANDLE,      /* the passed-in handle is not a valid CURLM handle */
    CURLM_BAD_EASY_HANDLE, /* an easy handle was not good/valid */
    CURLM_OUT_OF_MEMORY,   /* if you ever get this, you're in deep sh*t */
    CURLM_INTERNAL_ERROR,  /* this is a libcurl bug */
    CURLM_BAD_SOCKET,      /* the passed in socket argument did not match */
    CURLM_UNKNOWN_OPTION,  /* curl_multi_setopt() with unsupported option */
    CURLM_ADDED_ALREADY,   /* an easy handle already added to a multi handle was
                            attempted to get added - again */
    CURLM_RECURSIVE_API_CALL, /* an api function was called from inside a
                                callback */
    CURLM_WAKEUP_FAILURE,  /* wakeup is unavailable or failed */
    CURLM_BAD_FUNCTION_ARGUMENT,  /* function called with a bad parameter */
    CURLM_LAST
}

public enum CURLMoption
{
    /* This is the socket callback function pointer */
    CURLMOPT_SOCKETFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 1,
    /* This is the argument passed to the socket callback */
    CURLMOPT_SOCKETDATA = CURLOPTTYPE.OBJECTPOINT + 2,
    /* set to 1 to enable pipelining for this multi handle */
    CURLMOPT_PIPELINING = CURLOPTTYPE.LONG + 3,
    /* This is the timer callback function pointer */
    CURLMOPT_TIMERFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 4,
    /* This is the argument passed to the timer callback */
    CURLMOPT_TIMERDATA = CURLOPTTYPE.OBJECTPOINT + 5,
    /* maximum number of entries in the connection cache */
    CURLMOPT_MAXCONNECTS = CURLOPTTYPE.LONG + 6,
    /* maximum number of (pipelining) connections to one host */
    CURLMOPT_MAX_HOST_CONNECTIONS = CURLOPTTYPE.LONG + 7,
    /* maximum number of requests in a pipeline */
    CURLMOPT_MAX_PIPELINE_LENGTH = CURLOPTTYPE.LONG + 8,
    /* a connection with a content-length longer than this
       will not be considered for pipelining */
    CURLMOPT_CONTENT_LENGTH_PENALTY_SIZE = CURLOPTTYPE.OFF_T + 9,
    /* a connection with a chunk length longer than this
       will not be considered for pipelining */
    CURLMOPT_CHUNK_LENGTH_PENALTY_SIZE = CURLOPTTYPE.OFF_T + 10,
    /* a list of site names(+port) that are blocked from pipelining */
    CURLMOPT_PIPELINING_SITE_BL = CURLOPTTYPE.OBJECTPOINT + 11,
    /* a list of server types that are blocked from pipelining */
    CURLMOPT_PIPELINING_SERVER_BL = CURLOPTTYPE.OBJECTPOINT + 12,
    /* maximum number of open connections in total */
    CURLMOPT_MAX_TOTAL_CONNECTIONS = CURLOPTTYPE.LONG + 13,
    /* This is the server push callback function pointer */
    CURLMOPT_PUSHFUNCTION = CURLOPTTYPE.FUNCTIONPOINT + 14,
    /* This is the argument passed to the server push callback */
    CURLMOPT_PUSHDATA = CURLOPTTYPE.OBJECTPOINT + 15,
    /* maximum number of concurrent streams to support on a connection */
    CURLMOPT_MAX_CONCURRENT_STREAMS = CURLOPTTYPE.LONG + 16,
    CURLMOPT_LASTENTRY /* the last unused */
}

public enum CURLMSG
{
    CURLMSG_NONE, /* first, not used */
    CURLMSG_DONE, /* This easy handle has completed. 'result' contains
                   the CURLcode of the transfer */
    CURLMSG_LAST /* last, not used */
}

[StructLayout(LayoutKind.Explicit)]
public readonly struct CURLMsgData
{
    [FieldOffset(0)]
    public readonly nint whatever; /* (void*) message-specific data */
    [FieldOffset(0)]
    public readonly CURLcode result; /* return code for transfer */
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public readonly struct CURLMsg
{
    public readonly CURLMSG @type; /* what this message means */
    public readonly nint easy_handle; /* the handle it concerns */
    public readonly CURLMsgData data;
}

public static class libcurl
{
    private const string LibCurl = "libcurl";

    [DllImport(LibCurl)]
    public static extern CURLcode curl_global_init(
        long flags
    );

    [DllImport(LibCurl)]
    public static extern nint curl_easy_init();

    [DllImport(LibCurl)]
    public static extern void curl_easy_cleanup(
        nint easy
    );

    [DllImport(LibCurl)]
    public static extern CURLcode curl_easy_setopt(
        nint easy,
        CURLoption option,
        string? str
    );

    [DllImport(LibCurl)]
    public static extern CURLcode curl_easy_setopt(
         nint easy,
         CURLoption option,
         nint value
    );

    [DllImport(LibCurl)]
    public static extern CURLcode curl_easy_setopt(
        nint easy,
        CURLoption option,
        long value
    );

    [DllImport(LibCurl)]
    public static extern CURLcode curl_easy_setopt(
        nint easy,
        CURLoption option,
        int value
    );

    [DllImport(LibCurl)]
    public static extern void curl_easy_reset(
        nint easy
    );

    [DllImport(LibCurl)]
    public static extern nint curl_easy_strerror(
        CURLcode errornum
    );

    [DllImport(LibCurl)]
    public static extern nint curl_slist_append(
        nint list,
        string s
    );
    [DllImport(LibCurl)]
    public static extern void curl_slist_free_all(
        nint list
    );

    [DllImport(LibCurl)]
    public static extern CURLcode curl_easy_perform(
        nint easy
    );

    [DllImport(LibCurl)]
    public static extern nint curl_multi_init();

    [DllImport(LibCurl)]
    public static extern CURLMcode curl_multi_add_handle(
        nint multi_handle,
        nint curl_handle
    );

    [DllImport(LibCurl)]
    public static extern CURLMcode curl_multi_remove_handle(
        nint multi_handle,
        nint curl_handle
    );

    [DllImport(LibCurl)]
    public static extern CURLMcode curl_multi_poll(
        nint multi_handle,
        nint extra_fds,
        uint extra_nfds,
        int timeout_ms,
        out int numfds
    );

    [DllImport(LibCurl)]
    public static extern CURLMcode curl_multi_wakeup(
        nint multi_handle
    );

    [DllImport(LibCurl)]
    public static extern CURLMcode curl_multi_perform(
        nint multi_handle,
        out int running_handles
    );

    [DllImport(LibCurl)]
    public static extern nint curl_multi_info_read(
        nint multi_handle,
        out int msgs_in_queue
    );

    [DllImport(LibCurl)]
    public static extern CURLMcode curl_multi_cleanup(
        nint multi_handle
    );


    [DllImport(LibCurl)]
    public static extern nint curl_multi_strerror(
        CURLMcode errornum
    );

    [DllImport(LibCurl)]
    public static extern CURLMcode curl_multi_setopt(
        nint multi,
        CURLMoption option,
        string? str
    );

    [DllImport(LibCurl)]
    public static extern CURLMcode curl_multi_setopt(
        nint multi,
        CURLMoption option,
        long value
    );

    [DllImport(LibCurl)]
    public static extern CURLcode curl_easy_getinfo(
        nint curl,
        CURLINFO info,
        out IntPtr ptr
    );

    [DllImport(LibCurl)]
    public static extern CURLcode curl_easy_getinfo(
        nint curl,
        CURLINFO info,
        out int value
    );

}