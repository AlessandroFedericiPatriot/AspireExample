using AspireExample.Infrastructure.Data;
using AspireExample.Infrastructure.Data.Interceptors;
using AspireExample.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnectionString)
    {
        services.AddScoped<IFileStorage, LocalFileStorage>();

        services.AddScoped<ISaveChangesInterceptor, TrackedEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditLogInterceptor>();

        services.AddDbContext<AspireExampleDbContext>(
            (sp, options) =>
            {
                options.UseNpgsql(dbConnectionString);

                var interceptors = sp.GetServices<ISaveChangesInterceptor>();   
                options.AddInterceptors(interceptors);
            });

        return services;
    }
}