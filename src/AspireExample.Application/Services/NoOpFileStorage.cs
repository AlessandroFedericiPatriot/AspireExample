using AspireExample.Application.Interfaces;
using AspireExample.Domain;
using Microsoft.Extensions.Logging;

namespace AspireExample.Application.Services;

public sealed class NoOpFileStorage(ILogger<NoOpFileStorage> logger) : IFileStorage
{
    int _currentId = 0;

    public Task<Result<int>> SaveAsync(string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken = default)
    {
        var nextValue = Interlocked.Increment(ref _currentId);
        
        logger.LogInformation("Saved file {FileName} with content type {ContentType} and size {Size}", fileName, contentType, fileStream.Length);

        return Task.FromResult(Result.Success(nextValue));
    }

    public Task<Result<int>> DeleteAsync(string? filter = default)
    {
        var count = Random.Shared.Next(1, 6);

        logger.LogInformation("Deleted {Count} files", count);

        return Task.FromResult(Result.Success(count));
    }
}