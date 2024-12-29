namespace SharedKernel;

public abstract class TrackedEntity<TId> : EntityBase<TId>
    where TId : struct, IEquatable<TId>
{
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public string CreatedBy { get; private set; } = string.Empty;
    public DateTime? UpdatedOn { get; private set; }
    public string UpdatedBy { get; private set; } = string.Empty;
}