using System.Reflection;
using FluentValidation;
using Infrastructure.Shared.Mediator.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared.Mediator.Extensions;

public static class RegisterMediator
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}