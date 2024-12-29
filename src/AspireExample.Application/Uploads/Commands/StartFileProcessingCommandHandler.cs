using AspireExample.Application.Interfaces;
using AspireExample.Domain;
using Microsoft.Extensions.Logging;

namespace AspireExample.Application.Uploads.Commands;

public class StartFileProcessingCommandHandler(IFileStorage fileStorage) 
    : ICommandHandler<StartFileProcessingCommand, Result<FileUploadId>>
{
    public async Task<Result<FileUploadId>> Handle(StartFileProcessingCommand request, CancellationToken cancellationToken)
    {        
        var fileId = await fileStorage.SaveAsync(
            request.FileName,
            request.ContentType,
            request.FileStream,
            cancellationToken);

        await Task.Delay(100, cancellationToken);

        return Result.Success(fileId);
    }
}
