using Microsoft.Extensions.DependencyInjection;

namespace Random.User.Domain.Extensions;

public static class Startup
{
    /// <summary>
    ///     Initializes the domain services and configurations.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    public static void InitDomain(this IServiceCollection services)
    {
        Console.WriteLine("");
    }
}