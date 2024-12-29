using AspireExample.Domain;

namespace AspireExample.Application.Interfaces;

public interface IFileStorage
{
    Task<Result<FileUploadId>> SaveAsync(string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken = default);
    Task<Result<int>> Delete(string? filter = default);
}
