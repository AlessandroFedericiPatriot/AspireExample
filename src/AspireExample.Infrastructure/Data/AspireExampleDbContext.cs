using AspireExample.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspireExample.Infrastructure.Data;

public class AspireExampleDbContext : DbContext
{    
    public AspireExampleDbContext(DbContextOptions<AspireExampleDbContext> options) : base(options)
    {
    }

    public DbSet<FileUpload> FileUploads { get; set; } = null!;
    public DbSet<FileDigest> FileDigests { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}