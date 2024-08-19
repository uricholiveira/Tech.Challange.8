using Infrastructure.Shared.Logging.Enrichers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.OpenTelemetry;

namespace Infrastructure.Shared.Logging.Extensions;

public static class SerilogExtension
{
    public static void AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        const string outputTemplate =
            "[{Timestamp:HH:mm:ss} {Level:u3}] [{RequestId}] {Message:lj}{NewLine}{Exception}";

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.WithThreadId()
            .Enrich.WithEnvironmentName()
            .Enrich.WithExceptionDetails()
            .Enrich.FromLogContext()
            .Enrich.With<ActivityEnricher>()
            .WriteTo.Console(outputTemplate: outputTemplate)
            .WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = configuration["OpenTelemetry:Endpoint"];
                options.Protocol = OtlpProtocol.HttpProtobuf;
                options.IncludedData = IncludedData.TraceIdField | IncludedData.SpanIdField |
                                       IncludedData.SourceContextAttribute;
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["app"] = "webapi",
                    ["runtime"] = "dotnet",
                    ["service.name"] = configuration["OpenTelemetry:ServiceName"]!
                };
            })
            .CreateLogger();

        services.AddLogging(builder => builder.AddSerilog());
    }
}