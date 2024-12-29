using AspireExample.Application.Interfaces;
using AspireExample.Application.Services;
using AspireExample.Domain;
using FluentAssertions;

namespace AspireExample.UnitTests.Mocks;

public class NoOpFileStorageTests(IFileStorage fileStorage)
{
    [Fact]
    public void Receives_NoOpFileStorage()
    {
        fileStorage.GetType().Should().Be(typeof(NoOpFileStorage));
    }

    [Fact]
    public async Task Saves_and_returns_greather_than_zero_number()
    {
        var result = await fileStorage.SaveAsync("file.txt", "text/plain", new MemoryStream(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeGreaterThan(FileUploadId.From(0));
    }
}
