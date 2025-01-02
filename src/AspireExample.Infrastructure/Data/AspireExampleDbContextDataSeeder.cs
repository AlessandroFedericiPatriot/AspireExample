using AspireExample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AspireExample.Infrastructure.Data;

public static class AspireExampleDbContextDataSeeder
{
    public static async Task SeedDataAsync(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AspireExampleDbContext>();

        // Rebuilds the DB
        context.Database.EnsureDeleted();
        await context.Database.MigrateAsync();        

        var fileUploads = new List<FileUpload>
        {
            FileUpload.CreateNew("file1.txt", new Uri("http://example1"), "text/plain", 100),
            FileUpload.CreateNew("file2.txt", new Uri("http://example2"), "text/plain", 200),
            FileUpload.CreateNew("file3.txt", new Uri("http://example3"), "text/plain", 300),
        };

        await context.FileUploads.AddRangeAsync(fileUploads);

        await context.SaveChangesAsync();

        var fileDigests = new List<FileDigest>
        {
            FileDigest.For(fileUploads[0], "Subject of file1", "Summary of file1", "Details of file1"),
            FileDigest.For(fileUploads[0], "Subject 2 of file1", "Summary 2 of file1", "Details 2 of file1"),
            FileDigest.For(fileUploads[1], "Subject of file2", "Summary of file2", "Details of file2"),
            FileDigest.For(fileUploads[2], "Subject of file3", "Summary of file3", "Details of file3"),

        };

        await context.FileDigests.AddRangeAsync(fileDigests);

        await context.SaveChangesAsync();

        // Tests

        var up1 = await context.FileUploads.FindAsync(fileUploads[0].Id);
        up1!.Size = 999;

        await context.SaveChangesAsync();
    }
}
