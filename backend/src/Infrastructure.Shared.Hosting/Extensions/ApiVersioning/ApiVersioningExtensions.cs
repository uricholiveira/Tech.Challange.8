﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared.Hosting.Extensions.ApiVersioning;

public static class ApiVersioningExtensions
{
    public static IServiceCollection SetupApiVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            })
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddEndpointsApiExplorer();

        return services;
    }
}