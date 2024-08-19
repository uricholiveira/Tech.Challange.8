using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Shared.Hosting.Extensions.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection SetupSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
            var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            options.IncludeXmlComments(xmlPath);
            options.OperationFilter<SwaggerDefaultValues>();

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.UseInlineDefinitionsForEnums();
            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
        });

        return services;
    }

    public static WebApplication UseSwaggerConfig(this WebApplication application,
        IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            var descriptions = apiVersionDescriptionProvider.ApiVersionDescriptions;

            foreach (var desc in descriptions)
            {
                var url = $"/swagger/{desc.GroupName}/swagger.json";
                var name = desc.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });

        return application;
    }
}