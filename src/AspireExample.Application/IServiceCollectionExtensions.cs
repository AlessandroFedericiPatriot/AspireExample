using System.Reflection;
using AspireExample.Application.Interfaces;
using AspireExample.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Behaviors;

namespace AspireExample.Application;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFileStorage, NoOpFileStorage>();

        // FluentValidation 
        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly(),
            includeInternalTypes: true); // this assembly

        // Mediator
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()); // this assembly

            // Behaviors (MediatR pipeline)
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        });

        // Domain Event Dispatcher (DDD)
        services.AddSingleton(TimeProvider.System);

        return services;
    }   
}
