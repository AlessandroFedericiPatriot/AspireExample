using System.Security.Claims;
using Ardalis.Result.AspNetCore;
using AspireExample.Application.Uploads.Commands;

namespace AspireExample.ApiService;

public static class FilesApiEndpoints
{
    public static WebApplication MapFilesApiEndpoints(this WebApplication app)
    {
        app.MapPost("/api/files/upload",
            async (IFormFile file, IMediator mediator, CancellationToken cancellationToken) =>
            {
                // TODO check this looks good in swagger
                Result<Domain.FileUploadId> result = await mediator.Send(
                    new StartFileProcessingCommand(
                        FileName: file.FileName,
                        ContentType: file.ContentType,
                        FileStream: file.OpenReadStream()), 
                    cancellationToken);

                return result.ToMinimalApiResult();                
            })
        .Accepts<IFormFile>("multipart/form-data")
        .Produces(200)
        .Produces(400)
        .Produces(401)
        .Produces(500)
        .WithName("UploadFile")
        .RequireAuthorization();

        return app;
    }
}