using AspireExample.Application.Interfaces;
using AspireExample.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AspireExample.Application;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFileStorage, NoOpFileStorage>();

        return services;
    }   
}
