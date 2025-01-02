using AspireExample.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel.EntityFrameworkCore;

namespace AspireExample.Infrastructure.Data;

public class AspireExampleDbContext : DbContextBase
{    
    public AspireExampleDbContext(DbContextOptions<AspireExampleDbContext> options) 
        : base(options)
    {
    }

    public DbSet<FileUpload> FileUploads { get; set; } = null!;
    public DbSet<FileDigest> FileDigests { get; set; } = null!;
}