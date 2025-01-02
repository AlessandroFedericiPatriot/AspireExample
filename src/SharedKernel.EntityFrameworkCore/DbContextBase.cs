using Microsoft.EntityFrameworkCore;

namespace SharedKernel.EntityFrameworkCore;

public abstract class DbContextBase : DbContext
{
    protected DbContextBase(DbContextOptions options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
