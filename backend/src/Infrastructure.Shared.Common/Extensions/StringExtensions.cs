using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Shared.Common.Extensions;

public static class StríngExtensions
{
    public static string ToSnakeCase(this string input)
    {
        return string.IsNullOrWhiteSpace(input) ? input : Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }

    public static string RemoveAllButNumbers(this string input)
    {
        return string.IsNullOrWhiteSpace(input) ? input : Regex.Replace(input, "[^0-9]", "").Trim();
    }

    public static string RemoveAllButAlphaSpaceAndUnderline(this string input)
    {
        return string.IsNullOrWhiteSpace(input) ? input : Regex.Replace(input, "[^0-9a-zA-Z _]", "").Trim();
    }

    public static string FormatCpf(this string input)
    {
        return Convert.ToUInt64(input.RemoveAllButNumbers()).ToString(@"000\.000\.000\-00");
    }

    public static string FormatCnpj(this string input)
    {
        return Convert.ToUInt64(input.RemoveAllButNumbers()).ToString(@"00\.000\.000/0000\-00");
    }

    public static string FormatPhoneNumber(this string input)
    {
        return Convert.ToUInt64(input.RemoveAllButNumbers()).ToString(@"(00) 00000\-0000");
    }

    public static string RemoveDiacritics(this string @this)
    {
        var normalizedString = @this.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var t in normalizedString)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(t);
            if (uc == UnicodeCategory.NonSpacingMark) continue;
            sb.Append(t);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    public static ulong? ParseToULong(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        if (!Regex.IsMatch(input, @"^\d+$"))
            throw new ArgumentException("Input must be a number.", nameof(input));

        return ulong.Parse(input);
    }

    public static uint? ParseToUInt(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        if (!Regex.IsMatch(input, @"^\d+$"))
            throw new ArgumentException("Input must be a number.", nameof(input));

        return uint.Parse(input);
    }

    public static ushort? ParseToUShort(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        if (!Regex.IsMatch(input, @"^\d+$"))
            throw new ArgumentException("Input must be a number.", nameof(input));

        return ushort.Parse(input);
    }

    public static DateOnly? ParseToDateOnly(this string input)
    {
        if (input.Length != 8)
            throw new InvalidDataException("Invalid date format. Expected ddMMYYYY.");

        if (input == "00000000" || string.IsNullOrWhiteSpace(input))
            return null;

        try
        {
            var dateOnly = DateOnly.ParseExact(input, "ddMMyyyy");
            return dateOnly;
        }
        catch
        {
            throw new InvalidDataException("Invalid date format. Expected ddMMYYYY.");
        }
    }
}