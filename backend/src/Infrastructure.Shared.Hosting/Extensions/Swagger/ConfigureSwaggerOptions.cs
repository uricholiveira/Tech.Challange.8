using System.Text;
using Infrastructure.Shared.Hosting.Extensions.Swagger.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Shared.Hosting.Extensions.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private const string DeprecatedMessage = "This API version has been deprecated.";
    private const string SwaggerSection = "Swagger";

    private readonly IApiVersionDescriptionProvider _provider;

    private readonly SwaggerOptions? _swaggerOptions = new()
        { Title = "Undefined Title", Description = "Undefined Description." };

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
    {
        _provider = provider;

        var swaggerSection = configuration.GetSection(SwaggerSection);

        _swaggerOptions = swaggerSection.Get<SwaggerOptions>() ?? _swaggerOptions;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription apiDescription)
    {
        var description = new StringBuilder(_swaggerOptions!.Description);
        var info = new OpenApiInfo
        {
            Title = _swaggerOptions!.Title,
            Version = apiDescription.ApiVersion.ToString()
        };

        if (apiDescription.IsDeprecated) description.Append(DeprecatedMessage);

        info.Description = description.ToString();

        return info;
    }
}