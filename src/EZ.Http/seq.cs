namespace EZ.Http;

using System.Text;

public class EZHttpHeaders : Dictionary<string, string>
{
    public EZHttpHeaders() : base(StringComparer.OrdinalIgnoreCase)
    {
    }

    public EZHttpHeaders(IDictionary<string, string> dictionary)
        : base(dictionary, StringComparer.OrdinalIgnoreCase)
    {
    }

    public EZHttpHeaders(IEnumerable<KeyValuePair<string, string>> collection)
        : base(collection, StringComparer.OrdinalIgnoreCase)
    {
    }

    public EZHttpHeaders(int capacity)
        : base(capacity, StringComparer.OrdinalIgnoreCase)
    {
    }

    internal static EZHttpHeaders OfRawData(
        IList<ReadOnlyMemory<byte>> headers)
    {
        Dictionary<string, StringBuilder> tmp =
            new(
                headers.Count,
                StringComparer.OrdinalIgnoreCase
            );
        foreach (var h in headers) {
            var idxOfDelim = h.Span.IndexOf((byte)':');
            if (idxOfDelim <= -1) {
                continue;
            }
            var (name, value) = (
                h.Slice(start: 0, length: idxOfDelim),
                h.Slice(start: idxOfDelim + 1)
                .TrimStart(trimElements: CurlCallbacks.SP.Span)
                .TrimEnd(trimElements: CurlCallbacks.CrLf.Span)
            );
            var (nameS, valueS) = (
                Encoding.UTF8.GetString(name.Span),
                Encoding.UTF8.GetString(value.Span)
            );

            if (!tmp.ContainsKey(nameS)) {
                tmp[nameS] = new StringBuilder(valueS.Length);
            } else {
                tmp[nameS].Append(", ");
            }

            tmp[nameS].Append(valueS);
        }

        var ret = new EZHttpHeaders(tmp.Count);
        foreach (var (name, value) in tmp) {
            ret[name] = value.ToString();
        }
        return ret;
    }
}
