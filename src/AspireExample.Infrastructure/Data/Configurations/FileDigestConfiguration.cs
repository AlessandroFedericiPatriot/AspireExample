using AspireExample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspireExample.Infrastructure.Data.Configurations;

public class FileDigestConfiguration : IEntityTypeConfiguration<FileDigest>
{
    public void Configure(EntityTypeBuilder<FileDigest> builder)
    {
        builder.ToTable("FileDigests");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(new FileDigestId.EfCoreValueConverter());
        builder.Property(e => e.UploadId)
            .IsRequired()
            .HasConversion(new FileUploadId.EfCoreValueConverter());
        builder.Property(e => e.Subject)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(e => e.Summary)
            .HasMaxLength(255);
        builder.Property(e => e.Details)
            .HasMaxLength(255);
        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(255);
    }
}