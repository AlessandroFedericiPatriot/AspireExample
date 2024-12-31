using AspireExample.Infrastructure.Data;
using AspireExample.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFileStorage, LocalFileStorage>();
        return services;
    }
}