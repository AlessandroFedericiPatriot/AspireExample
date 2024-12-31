
namespace AspireExample.Domain;

[ValueObject<int>(Conversions.EfCoreValueConverter)]
public partial struct FileDigestId;

public class FileDigest : TrackedEntity<FileDigestId>
{
    internal FileDigest() { /* For Entity Framework/HotChocolate */ }

    public FileDigest(FileUploadId uploadId, string subject, string? summary = default, string? details = default)
    {
        UpdateSubject(subject);

        this.UploadId = uploadId;
        this.Summary = summary;
        this.Details = details;        
    }

    public FileUploadId UploadId { get; private set; }
    public string Subject { get; private set; } = default!;
    public string? Summary { get; private set; }
    public string? Details { get; private set; }

    public void UpdateSubject(string subject)
    {
        this.Subject = Guard.Against.NullOrEmpty(subject, nameof(subject));
    }    
}