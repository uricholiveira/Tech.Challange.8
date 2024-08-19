using Microsoft.Extensions.DependencyInjection;

namespace Random.User.Application.Extensions;

public static class Startup
{
    /// <summary>
    ///     Initializes the services and configurations.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    public static void InitServices(this IServiceCollection services)
    {
        Console.WriteLine("");
    }
}