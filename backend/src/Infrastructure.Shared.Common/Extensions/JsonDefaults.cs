using System.Text.Json;
using System.Text.Json.Serialization;
using Infrastructure.Shared.Common.Serialization.Converters;

namespace Infrastructure.Shared.Common.Extensions;

public static class JsonDefaults
{
    public static readonly JsonSerializerOptions JsonOpts;

    static JsonDefaults()
    {
        JsonOpts = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        JsonOpts.Converters.Add(new JsonEnumStringConverter());
    }
}