
namespace AspireExample.Domain;

[ValueObject<int>]
public partial struct FileDigestId;

public class FileDigest : TrackedEntity<FileDigestId>
{
    internal FileDigest() { /* For Entity Framework/HotChocolate */ }

    public FileDigest(FileUploadId uploadId, string subject, string? summary, string? details)
    {
        this.UploadId = uploadId;
        this.Subject = Guard.Against.NullOrEmpty(subject, nameof(subject));
        this.Summary = summary;
        this.Details = details;
    }

    public FileUploadId UploadId { get; private set; }
    public string Subject { get; private set; } = default!;
    public string? Summary { get; private set; } = default!;
    public string? Details { get; private set; }

    public void UpdateSubject(string subject)
    {
        this.Subject = Guard.Against.NullOrEmpty(subject, nameof(subject));
    }

    public void UpdateSummary(string summary)
    {
        this.Summary = Guard.Against.NullOrEmpty(summary, nameof(summary));
    }
}