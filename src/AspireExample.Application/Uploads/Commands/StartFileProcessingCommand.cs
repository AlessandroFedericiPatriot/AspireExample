using AspireExample.Domain;

namespace AspireExample.Application.Uploads.Commands;

public record class StartFileProcessingCommand(
    string FileName,
    string ContentType,
    Stream FileStream) : ICommand<Result<FileUploadId>>
{
    #region Validation

    internal class StartFileProcessingValidator : AbstractValidator<StartFileProcessingCommand>
    {
        private static readonly string[] AllowedMimeTypes =
        {
            "application/pdf",
            "image/jpeg",
            "image/png"
        };

        public StartFileProcessingValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty();
            
            RuleFor(x => x.ContentType)
                .NotEmpty()
                .Must(value => AllowedMimeTypes.Contains(value, StringComparer.InvariantCultureIgnoreCase))
                .WithMessage($"ContentType must be one of the following: {string.Join(", ", AllowedMimeTypes)}");

            RuleFor(x => x.FileStream)
                .NotNull();
        }
    }

    #endregion
}