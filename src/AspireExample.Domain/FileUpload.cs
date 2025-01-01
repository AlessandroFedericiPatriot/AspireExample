namespace AspireExample.Domain;



public readonly record struct FileUploadId(int Value) 
{
    public static FileUploadId Empty { get; } = default;
    public static FileUploadId From(int value) => new(value);
}

public class FileUpload : TrackedEntityBase, IEntity<FileUploadId>
{
    public static FileUpload CreateNew(string fileName, Uri location, string contentType, long size)
    {
        return new FileUpload
        {
            FileName = Guard.Against.NullOrEmpty(fileName, nameof(fileName)),
            Location = Guard.Against.Null(location, nameof(location)),
            ContentType = Guard.Against.NullOrEmpty(contentType, nameof(contentType)),
            Size = Guard.Against.NegativeOrZero(size, nameof(size))
        };
    }

    private FileUpload() { /* For Entity Framework/HotChocolate */ }

    // IEntity
    public FileUploadId Id { get; private set; } = FileUploadId.Empty;

    // Specific to FileUpload
    public string FileName { get; private set; } = default!;
    public Uri Location { get; private set; } = default!;
    public string ContentType { get; private set; } = default!;
    public long Size { get; private set; }
}