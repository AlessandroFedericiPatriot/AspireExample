using Ardalis.Result;

namespace AspireExample.Infrastructure.Services;

public class LocalFileStorage : IFileStorage
{
    public Task<Result<int>> DeleteAsync(string? filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<Result<int>> SaveAsync(string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
