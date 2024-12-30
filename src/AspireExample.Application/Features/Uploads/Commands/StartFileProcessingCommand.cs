using System.Reflection.Metadata;
using AspireExample.Domain;

namespace AspireExample.Application.Features.Uploads.Commands;

public record class StartFileProcessingCommand(
    string FileName,
    string ContentType,
    Stream FileStream) : ICommand<Result<int>>
{
    public static readonly string[] AllowedMimeTypes =
    {
        ApplicationPdf,
        ImageJpeg,
        ImagePng,
        TextPlain
    };

    public const string ApplicationPdf = "application/pdf";
    public const string ImageJpeg = "image/jpeg";
    public const string ImagePng = "image/png";
    public const string TextPlain = "text/plain";

    #region Validation

    internal class StartFileProcessingValidator : AbstractValidator<StartFileProcessingCommand>
    {
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