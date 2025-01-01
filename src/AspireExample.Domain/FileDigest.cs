namespace AspireExample.Domain;

public readonly record struct FileDigestId(int Value)
{
    public static FileDigestId Empty { get; } = default;
    public static FileDigestId From(int value) => new(value);
}

public class FileDigest : TrackedEntityBase, IEntity<FileDigestId>
{
    public static FileDigest For(FileUploadId fileUploadId, string subject, string? summary = default, string? details = default)
    {
        var result = new FileDigest
        {
            UploadId = fileUploadId,
            Subject = subject,
            Summary = summary,
            Details = details
        };

        return result;
    }

    public static FileDigest For(FileUpload fileUpload, string subject, string? summary = default, string? details = default)
        => For(fileUpload.Id, subject, summary, details);

    private FileDigest() { /* For Entity Framework/HotChocolate */ }

    // IEntity
    public FileDigestId Id { get; private set; } = default;

    // Specific to FileDigest
    public FileUploadId UploadId { get; private set; } = default!;
    public string Subject { get; private set; } = default!;
    public string? Summary { get; private set; }
    public string? Details { get; private set; }    
}