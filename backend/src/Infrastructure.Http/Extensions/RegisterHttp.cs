using Infrastructure.Http.Interfaces;
using Infrastructure.Http.Services;
using Infrastructure.Http.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Http.Extensions;

public static class RegisterHttp
{
    public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        MappingConfig.RegisterHttpMappings();

        services.AddHttpClient<IRandomUserApi, RandomUserApi>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("ExternalServices:RandomUserApi:Url")!);
        });
    }
}