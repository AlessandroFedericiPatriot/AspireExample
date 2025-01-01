using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspireExample.Infrastructure.Data.Configurations;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TTrackedEntity> ConfigureTrackedEntity<TTrackedEntity>(this EntityTypeBuilder<TTrackedEntity> builder)
        where TTrackedEntity : class, ITrackedEntity
    {
        builder.Property(e => e.CreatedOn)
               .HasDefaultValueSql("now()");

        builder.Property(e => e.CreatedBy);
        builder.Property(e => e.UpdatedOn);
        builder.Property(e => e.UpdatedBy);

        return builder;
    }

    public static EntityTypeBuilder<TEntity> ConfigureEntity<TEntity, TId>(this EntityTypeBuilder<TEntity> builder,
        Expression<Func<int, TId>> x,
        Expression<Func<TId, int>> y
        )
        where TEntity : class, IEntity<TId>
        where TId : struct, IEquatable<TId>
     {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasIdentityOptions(startValue: 1)
            .HasConversion(y, x);

        return builder;
    }
}
