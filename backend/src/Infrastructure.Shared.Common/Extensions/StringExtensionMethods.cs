using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Infrastructure.Shared.Common.Extensions;

public static class StringExtensionMethods
{
    public static T? FromJson<T>(this string str)
    {
        return JsonSerializer.Deserialize<T>(str, JsonDefaults.JsonOpts);
    }

    public static string ToJson<T>(this T o)
    {
        return JsonSerializer.Serialize(o, JsonDefaults.JsonOpts);
    }

    public static T? FromJson<T>(this string str, JsonSerializerOptions jsonOpts)
    {
        return JsonSerializer.Deserialize<T>(str, jsonOpts);
    }

    public static string ToJson<T>(this T o, JsonSerializerOptions jsonOpts)
    {
        return JsonSerializer.Serialize(o, jsonOpts);
    }

    public static string ToSnakeCase(this string input)
    {
        return string.IsNullOrEmpty(input)
            ? input
            : Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }

    public static byte[] GetHash(string inputString)
    {
        using var hashAlgorithm = SHA256.Create();
        return hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(this string inputString)
    {
        return BitConverter.ToString(GetHash(inputString)).Replace("-", string.Empty);
    }

    public static double ToDouble(this string inputString)
    {
        return string.IsNullOrWhiteSpace(inputString)
            ? 0
            : Convert.ToDouble(inputString.Trim().Replace(",", "."), CultureInfo.InvariantCulture);
    }

    public static bool Contains(this string source, string toCheck, bool caseSensitive = true)
    {
        return caseSensitive
            ? source.Contains(toCheck)
            : source.Contains(toCheck, StringComparison.OrdinalIgnoreCase);
    }
}