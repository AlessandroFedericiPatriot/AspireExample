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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AspireExampleDbContext).Assembly);

        // TODO: create multiple sequences instead of just one
        // TODO: make sure each configuration type gets the right sequence automagically somehow
        //modelBuilder.HasSequence<int>(GlobalSequenceName)
        //    .StartsAt(1)
        //    .IncrementsBy(1);
    }
}
