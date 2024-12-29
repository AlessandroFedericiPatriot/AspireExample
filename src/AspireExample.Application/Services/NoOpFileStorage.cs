using Ardalis.Result;
using AspireExample.Application.Interfaces;
using AspireExample.Domain;

namespace AspireExample.Application.Services;

public sealed class NoOpFileStorage : IFileStorage
{
    int _currentId = 0;

    public Task<Result<FileUploadId>> SaveAsync(string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success(FileUploadId.From(1)));
    }

    public Task<Result<int>> Delete(string? filter = default)
    {
        var nextValue = Interlocked.Increment(ref _currentId);

        return Task.FromResult(Result.Success(nextValue));
    }
}