using AspireExample.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel.EntityFrameworkCore;

namespace AspireExample.Infrastructure.Data;

public class AspireExampleDbContext(DbContextOptions<AspireExampleDbContext> options) 
    : DbContextBase(options)
{
    public DbSet<FileUpload> FileUploads { get; set; } = null!;
    public DbSet<FileDigest> FileDigests { get; set; } = null!;
}