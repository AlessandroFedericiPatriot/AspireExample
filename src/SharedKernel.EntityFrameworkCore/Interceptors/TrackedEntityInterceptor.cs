using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;

namespace AspireExample.Infrastructure.Data.Interceptors;

public class TrackedEntityInterceptor(
    IUserContext user,
    TimeProvider dateTime) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        MarkTrackedEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        MarkTrackedEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void MarkTrackedEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<ITrackedEntity>())
        {
            var utcNow = dateTime.GetUtcNow();
            var uid = user.UserId ?? "UNSPECIFIED";

            // TODO: I am not entirely sure HasChangedOwnedEntities() plays any role or not in Added
            if (entry.State is EntityState.Added/* || entry.HasChangedOwnedEntities()*/)
            {
                entry.Entity.MarkCreated(utcNow, uid);
            }
            else if (entry.State is EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.MarkUpdated(utcNow, uid);
            }
        }
    }
}

internal static class EntityEntryExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
