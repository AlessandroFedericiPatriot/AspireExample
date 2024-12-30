using AspireExample.Infrastructure.Services;

namespace AspireExample.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFileStorage, LocalFileStorage>();
        return services;
    }   
}
