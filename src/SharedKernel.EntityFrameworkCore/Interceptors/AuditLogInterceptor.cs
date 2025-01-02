using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;

namespace AspireExample.Infrastructure.Data.Interceptors;

public class AuditLogInterceptor(
    IUserContext userContext,
    TimeProvider timeProvider,
    ILogger<AuditLogInterceptor> logger) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        LogAuditLogEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        LogAuditLogEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void LogAuditLogEntities(DbContext? context)
    {
        if (context == null) return;
        foreach (var entry in context.ChangeTracker.Entries<IAuditLog>())
        {
            var utcNow = timeProvider.GetUtcNow();
            var uid = userContext.UserId ?? "UNSPECIFIED";

            if (entry.State is EntityState.Added)
            {
                logger.LogEntityCreation(entry.Entity);

                // Telemetry: Log the creation of an entity
            }
            else if (entry.State is EntityState.Modified)
            {
                // TODO: just log what changed
                logger.LogEntityUpdate(entry.Entity);//, entry.OriginalValues, entry.CurrentValues);

                // Telemetry: Log the difference between the original and current values of an entity                
            }
            else if (entry.State is EntityState.Deleted)
            {                
                logger.LogEntityDeletion(entry.Entity);

                // Telemetry: Log the deletion of an entity
            }
        }
    }    
}

public static partial class AuditLogInterceptorLogging
{
    [LoggerMessage(EventId = 1000, Level = LogLevel.Information, Message = "{@entity} has been created.")]
    public static partial void LogEntityCreation(this ILogger logger, object entity);

    [LoggerMessage(EventId = 1001, Level = LogLevel.Information, Message = "{@entity} has been deleted.")]
    public static partial void LogEntityDeletion(this ILogger logger, object entity);

    [LoggerMessage(EventId = 1003, Level = LogLevel.Information, Message = "{@entity} has been updated.")]
    public static partial void LogEntityUpdate(this ILogger logger, object entity);//, PropertyValues originalValues, PropertyValues newValues);
}

//foreach (var prop in entry.OriginalValues.Properties) 
//{
//    var orig_value = entry.OriginalValues[prop.Name];
//    var new_value = entry.CurrentValues[prop.Name];
//
//    if (new_value != null && !new_value.Equals(orig_value))
//    {
//        logger.LogInformation($"Property {prop.Name} has changed from {orig_value} to {new_value}.");
//    }
//}