namespace AspireExample.Domain;

public readonly record struct FileDigestId(int Value)
{
    public static FileDigestId Empty { get; } = default;
    public static FileDigestId From(int value) => new(value);
}

public class FileDigest(
    FileUploadId uploadId, 
    string subject, 
    string? summary = default, 
    string? details = default) 

    : TrackedEntityBase, IEntity<FileDigestId>, IAuditLog
{
    public static FileDigest For(FileUpload fileUpload, string subject, string? summary = default, string? details = default)
        => new (fileUpload.Id, subject, summary, details);

    // IEntity
    public FileDigestId Id { get; private set; }

    // Specific to FileDigest
    public FileUploadId UploadId { get; private set; } = uploadId;
    public string Subject { get; private set; } = !string.IsNullOrWhiteSpace(subject) ? subject : throw new ArgumentNullException(nameof(subject));
    public string? Summary { get; private set; } = summary;
    public string? Details { get; private set; } = details;
}