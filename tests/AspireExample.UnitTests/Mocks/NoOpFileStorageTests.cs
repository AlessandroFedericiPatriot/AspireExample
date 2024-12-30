namespace AspireExample.UnitTests.Mocks;

public class NoOpFileStorageTests(
    IFileStorage fileStorage)
{
    [Fact]
    public void Receives_NoOpFileStorage()
    {        
        fileStorage.GetType().Should().Be<NoOpFileStorage>();
    }

    [Fact]
    public async Task Saves_and_returns_greather_than_zero_number()
    {
        var result = await fileStorage.SaveAsync("file.txt", "text/plain", new MemoryStream(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Deletes_and_returns_greather_than_zero_number()
    {
        var result = await fileStorage.DeleteAsync();

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeGreaterThan(0);
    }
}