using AspireExample.Infrastructure.Data;
using AspireExample.Infrastructure.Data.Interceptors;
using AspireExample.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFileStorage, LocalFileStorage>();
        
        //services.AddScoped<ISaveChangesInterceptor, TrackedEntityInterceptor>();

        return services;
    }
}