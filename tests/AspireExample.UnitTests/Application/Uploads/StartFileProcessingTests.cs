using System.Text;
using AspireExample.Application.Features.Uploads.Commands;
using FluentValidation;
using MediatR;

namespace AspireExample.UnitTests.Application.Uploads;

public class StartFileProcessingTests(IMediator mediator)
{
    [Fact]
    public async Task StartFileProcessingCommandHandler_ShouldSaveFile()
    {
        var command = new StartFileProcessingCommand(
            "test.txt",
            StartFileProcessingCommand.TextPlain,
            new MemoryStream(Encoding.UTF8.GetBytes("Test file")));

        var result = await mediator.Send(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task StartFileProcessingCommandHandler_should_fail_with_invalid_mimetype()
    {
        var command = new StartFileProcessingCommand(
            "test.txt",
            "bad_type",
            new MemoryStream(Encoding.UTF8.GetBytes("Test file")));

        var act =  () => mediator.Send(command, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>();        
    }
}
