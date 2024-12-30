namespace AspireExample.Application.Interfaces;

public interface IFileStorage
{
    Task<Result<int>> SaveAsync(string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken = default);
    Task<Result<int>> DeleteAsync(string? filter = default);
}
