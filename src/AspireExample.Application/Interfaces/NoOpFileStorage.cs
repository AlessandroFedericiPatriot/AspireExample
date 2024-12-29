using AspireExample.Domain;

namespace AspireExample.Application.Interfaces;

public sealed class NoOpFileStorage : IFileStorage
{
    public Task<Result<FileUploadId>> SaveAsync(string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success(FileUploadId.From(1)));
    }

    public Task<Result<int>> Delete(string? filter = default)
    {
        return Task.FromResult(Result.Success(1));
    }
}