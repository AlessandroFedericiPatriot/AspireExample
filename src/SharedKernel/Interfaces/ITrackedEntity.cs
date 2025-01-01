namespace SharedKernel;

public interface ITrackedEntity
{
    string CreatedBy { get; }
    DateTimeOffset CreatedOn { get; }
    string? UpdatedBy { get; }
    DateTimeOffset? UpdatedOn { get; }

    void MarkCreated(DateTimeOffset createdOn, string createdBy);
    void MarkUpdated(DateTimeOffset updatedOn, string updatedBy);
}