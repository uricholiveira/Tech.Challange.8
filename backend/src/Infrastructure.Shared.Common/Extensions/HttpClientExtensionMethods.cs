using System.Data;
using System.Text.Json;

namespace Infrastructure.Shared.Common.Extensions;

public static class HttpClientExtensionMethods
{
    public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content, JsonSerializerOptions? jsonOpts = null)
    {
        return await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync(),
            jsonOpts ?? JsonDefaults.JsonOpts) ?? throw new NoNullAllowedException("Deserialized object is null");
    }
}