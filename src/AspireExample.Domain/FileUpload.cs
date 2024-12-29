namespace AspireExample.Domain;

[ValueObject<int>]
public partial struct FileUploadId;

public class FileUpload : TrackedEntity<FileUploadId>
{
    public FileUpload(string fileName, Uri location, string contentType, long fileSize)
    {
        this.FileName = fileName;
        this.Location = location;
        this.ContentType = contentType;
        this.FileSize = fileSize;
    }

    public string FileName { get; private set; }
    public Uri Location { get; private set; }
    public string ContentType { get; private set; }
    public long FileSize { get; private set; }
}

