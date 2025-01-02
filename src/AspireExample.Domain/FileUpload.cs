namespace AspireExample.Domain;

public readonly record struct FileUploadId(int Value) 
{
    public static FileUploadId Empty { get; } = default;
    public static FileUploadId From(int value) => new(value);
}

public class FileUpload(
    string fileName, 
    Uri location, 
    string contentType, 
    long size) 

    : TrackedEntityBase, IEntity<FileUploadId>, IAuditLog
{
    public static FileUpload CreateNew(string fileName, Uri location, string contentType, long size)
    {
        return new FileUpload(fileName, location, contentType, size);        
    }

    // IEntity
    public FileUploadId Id { get; private set; }

    // Specific to FileUpload
    public string FileName { get; private set; } = !string.IsNullOrWhiteSpace(fileName) ? fileName : throw new ArgumentNullException(nameof(fileName));    
    public Uri Location { get; private set; } = location ?? throw new ArgumentNullException(nameof(location));

    public string ContentType { get; private set; } = !string.IsNullOrWhiteSpace(contentType) ? contentType : throw new ArgumentNullException(nameof(contentType));

    public long Size { get; set; } = size > 0 ? size : throw new ArgumentOutOfRangeException(nameof(size));
}