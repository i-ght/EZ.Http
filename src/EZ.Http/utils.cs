using System.Runtime.InteropServices;
using System.Text;

namespace EZ.Http;

internal static class Utils
{
    public static bool StrCaseInsesEq(
        string a,
        string b
    ) =>
        string.Equals(
            a,
            b,
            StringComparison.OrdinalIgnoreCase
        );

    private static byte ChangeByteToLower(byte value) =>
        value >= (byte)'A' && value <= (byte)'Z'
            ? value += (byte)'a' - (byte)'A'
            : value;

    public static unsafe int BytesIndexOfCaseInsens(
        in ReadOnlySpan<byte> input,
        in ReadOnlySpan<byte> value)
    {
        if (value.Length > input.Length) {
            return -1;
        }

        if (value.Length == 0) {
            return 0;
        }

        var startOfValueIndex = 0 ;
        var valueIndex = 0;
        var _0thValue = ChangeByteToLower(value[0]);

        for (var i = 0; i < input.Length; i++) {
            var a = ChangeByteToLower(input[i]);
            var b = ChangeByteToLower(value[valueIndex]);

            if (a != b) {
                valueIndex = 0;
                /* go back one and recheck for the value */
                if (a == _0thValue) {
                    i--;
                }
                continue;
            }

            if (0 == valueIndex++) {
                /* at first char */
                startOfValueIndex = i;
            }

            /* found entire value */
            if (valueIndex == value.Length) {
                return startOfValueIndex;
            }
        }
        return -1;
    }

    public static bool BytesStartsWithCaseInsens(
        in ReadOnlySpan<byte> input,
        in ReadOnlySpan<byte> value)
    {
        if (value.Length > input.Length) {
            return false;
        }

        for (var i = 0; i < value.Length; i++) {
            var a = Utils.ChangeByteToLower(input[i]);
            var b = Utils.ChangeByteToLower(value[i]);

        }

        return true;
    }
}
