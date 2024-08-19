using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Infrastructure.Shared.Common.Serialization.Attributes;
using Infrastructure.Shared.Common.Serialization.Enums;

namespace Infrastructure.Shared.Common.Serialization.Converters;

public class JsonEnumStringConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var deserializeAttribute = typeToConvert.GetCustomAttribute<DeserializeEnumAsAttribute>();
        var casingOption = deserializeAttribute?.CasingOption ?? CasingOption.CamelCase;

        switch (casingOption)
        {
            case CasingOption.CamelCase:
                var camelCasingConverter = new JsonStringEnumConverter(JsonNamingPolicy.CamelCase);
                return camelCasingConverter.CreateConverter(typeToConvert, options);
            case CasingOption.ShoutingCase:
                var converter = CreateShoutingCaseConverter(typeToConvert, options);
                return converter;
            default: throw new JsonException("The selected casing option is not handled");
        }
    }

    private static JsonConverter CreateShoutingCaseConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converter = (JsonConverter)Activator.CreateInstance(
            typeof(JsonShoutingCaseEnumStringConverter<>).MakeGenericType(typeToConvert),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
            null,
            null,
            null)!;

        return converter;
    }
}