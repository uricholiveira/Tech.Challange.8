namespace Infrastructure.Shared.Hosting.Extensions;

public static class EnvironmentExtensions
{
    public static bool IsProduction(this string? environment)
    {
        if (string.IsNullOrWhiteSpace(environment))
            return false;

        return environment.ToLower() == "production" || environment.ToLower() == "live";
    }

    public static bool IsStaging(this string? environment)
    {
        if (string.IsNullOrWhiteSpace(environment))
            return false;

        return environment.ToLower() == "staging" || environment.ToLower() == "cdev";
    }

    public static bool IsDevelopment(this string? environment)
    {
        if (string.IsNullOrWhiteSpace(environment))
            return false;

        return environment.ToLower() == "ldev" || environment.ToLower() == "development";
    }
}