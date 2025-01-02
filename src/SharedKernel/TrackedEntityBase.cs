using System.Runtime.InteropServices;
using SharedKernel.Interfaces;

namespace SharedKernel;

/// <summary>
/// Base class for entities that need to be tracked.
/// </summary>
public class TrackedEntityBase : HasDomainEventsBase, ITrackedEntity
{
    public string CreatedBy { get; private set; } = default!;
    public DateTimeOffset CreatedOn { get; private set; } = default!;
    public string? UpdatedBy { get; private set; } = default!;
    public DateTimeOffset? UpdatedOn { get; private set; } = default!;

    public void MarkCreated(DateTimeOffset createdOn, string createdBy)
    {
        CreatedOn = createdOn;
        CreatedBy = createdBy;
    }

    public void MarkUpdated(DateTimeOffset updatedOn, string updatedBy)
    {
        UpdatedOn = updatedOn;
        UpdatedBy = updatedBy;
    }
}
