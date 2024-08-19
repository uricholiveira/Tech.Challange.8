using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Shared.Common.Serialization.Converters;

internal class JsonShoutingCaseEnumStringConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var token = reader.GetString();

        var capitalizedToken = CamelCaseToShoutingCase(token!);

        return Enum.TryParse<TEnum>(capitalizedToken, out var result) ? result : default;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        var camelCased = ShoutingCaseToCamelCase(value.ToString());
        writer.WriteStringValue(camelCased);
    }

    private static string CamelCaseToShoutingCase(string camelCase)
    {
        var sb = new StringBuilder(camelCase.Length);
        for (var i = 0; i < camelCase.Length; i++)
        {
            var c = camelCase[i];
            if (i > 0 && char.IsLower(camelCase[i - 1]) && char.IsUpper(c)) sb.Append('_');

            sb.Append(char.ToUpper(c));
        }

        return sb.ToString();
    }

    private static string ShoutingCaseToCamelCase(string shoutingCase)
    {
        var sb = new StringBuilder(shoutingCase.Length);
        for (var i = 0; i < shoutingCase.Length; i++)
        {
            var c = shoutingCase[i];

            if (i > 0 && shoutingCase[i - 1] == '_')
                c = char.ToUpper(c);
            else
                c = char.ToLower(c);

            if (c != '_')
                sb.Append(c);
        }

        return sb.ToString();
    }
}