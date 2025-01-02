using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;

namespace AspireExample.Infrastructure.Data.Interceptors;

public class AuditLogInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext _user;
    private readonly TimeProvider _dateTime;
    private readonly ILogger<AuditLogInterceptor> _logger;

    public AuditLogInterceptor(
        IUserContext user,
        TimeProvider dateTime,
        ILogger<AuditLogInterceptor> logger)
    {
        _user = user;
        _dateTime = dateTime;
        _logger = logger;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        foreach (var entry in context.ChangeTracker.Entries<IAuditLog>())
        {
            var utcNow = _dateTime.GetUtcNow();
            var uid = _user.UserId ?? "UNSPECIFIED";

            if (entry.State is EntityState.Added)
            {
                foreach (var prop in entry.CurrentValues.Properties)
                {
                    var orig_value = entry.OriginalValues[prop.Name];
                    var new_value = entry.CurrentValues[prop.Name];

                    if (new_value != null && !new_value.Equals(orig_value))
                    {
                        _logger.LogInformation($"Property {prop.Name} has changed from {orig_value} to {new_value}.");
                    }
                }
                // Telemetry: Log the creation of an entity

            }
            else if (entry.State is EntityState.Modified)
            {                                
                foreach (var prop in entry.OriginalValues.Properties)
                {
                    var orig_value = entry.OriginalValues[prop.Name];
                    var new_value = entry.CurrentValues[prop.Name];

                    if (new_value != null && !new_value.Equals(orig_value))
                    {
                        _logger.LogInformation($"Property {prop.Name} has changed from {orig_value} to {new_value}.");
                    }
                }

                // Telemetry: Log the difference between the original and current values of an entity
            }
        }
    }
}
