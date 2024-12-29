namespace AspireExample.Domain;

[ValueObject<int>]
public partial struct FileDigestId;

public class FileDigest : TrackedEntity<FileDigestId>
{
    internal FileDigest() { /* For Entity Framework/HotChocolate */ }

    public FileDigest(FileUploadId uploadId, string subject, string summary)
    {
        this.UploadId = uploadId;
        this.Subject = subject;
        this.Summary = summary;
    }

    public FileUploadId UploadId { get; private set; }
    public string Subject { get; private set; } = default!;
    public string Summary { get; private set; } = default!;
    public string? Details { get; private set; }
}