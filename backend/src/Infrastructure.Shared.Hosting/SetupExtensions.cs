using Infrastructure.Shared.Hosting.Extensions.ApiVersioning;
using Infrastructure.Shared.Hosting.Extensions.Swagger;
using Infrastructure.Shared.Logging.Extensions;
using Infrastructure.Shared.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Shared.Hosting;

public static class SetupExtensions
{
    public static void SetupMicroservice(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        builder.Services.SetupApiVersioning();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.SetupSwagger();
        builder.Services.AddSerilog(configuration);
    }

    public static void FinishSetup(this WebApplication app)
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwaggerConfig(apiVersionDescriptionProvider);
        app.UseMiddleware<ExceptionMiddleware>();
    }
}