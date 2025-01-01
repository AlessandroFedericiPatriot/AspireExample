using AspireExample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharedKernel;

namespace AspireExample.Infrastructure.Data.Configurations;

public class FileDigestConfiguration : IEntityTypeConfiguration<FileDigest>
{
    public const string TableName = "FileDigests";

    public void Configure(EntityTypeBuilder<FileDigest> builder)
    {
        builder.ToTable(TableName, DbConstants.FileProcessorSchema);

        builder.ConfigureEntity(id => new FileDigestId(id), id => id.Value);
        builder.ConfigureTrackedEntity();
        
        builder.HasOne<FileUpload>()
            .WithMany()
            .HasForeignKey(e => e.UploadId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Subject)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Summary)
            .HasMaxLength(255);

        builder.Property(e => e.Details)
            .HasMaxLength(255);
    }
}