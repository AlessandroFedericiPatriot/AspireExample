using AspireExample.Application.Interfaces;
using AspireExample.Domain;
using Microsoft.Extensions.Logging;

namespace AspireExample.Application.Features.Uploads.Commands;

public class StartFileProcessingCommandHandler(IFileStorage fileStorage)
    : ICommandHandler<StartFileProcessingCommand, Result<int>>
{
    public async Task<Result<int>> Handle(StartFileProcessingCommand request, CancellationToken cancellationToken)
    {
        var fileId = await fileStorage.SaveAsync(
            request.FileName,
            request.ContentType,
            request.FileStream,
            cancellationToken);

        return Result.Success(fileId.Value);
    }
}
