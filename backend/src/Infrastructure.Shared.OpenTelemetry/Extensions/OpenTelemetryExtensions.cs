using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Infrastructure.Shared.OpenTelemetry.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection RegisterOpenTelemetry(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(configuration["OpenTelemetry:ServiceName"]!))
            .WithTracing(builder =>
            {
                builder
                    .AddAspNetCoreInstrumentation(options => { options.RecordException = true; })
                    .AddHttpClientInstrumentation(options => { options.RecordException = true; })
                    .AddEntityFrameworkCoreInstrumentation(x => x.SetDbStatementForText = true)
                    .AddNpgsql()
                    .AddOtlpExporter(opts =>
                    {
                        opts.Endpoint = new Uri(configuration["OpenTelemetry:Endpoint"]!);
                        opts.Protocol = OtlpExportProtocol.HttpProtobuf;
                    });
            });


        return services;
    }
}