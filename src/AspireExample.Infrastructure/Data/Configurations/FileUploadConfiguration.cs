using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspireExample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspireExample.Infrastructure.Data.Configurations;

public class FileUploadConfiguration : IEntityTypeConfiguration<FileUpload>
{
    public const string TableName = "FileUploads";

    public void Configure(EntityTypeBuilder<FileUpload> builder)
    {
        builder.ToTable(TableName, DbConstants.FileProcessorSchema);

        builder.ConfigureEntity(id => new FileUploadId(id), id => id.Value);
        builder.ConfigureTrackedEntity();        
        
        builder.Property(e => e.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Location)
            .IsRequired();
        builder.Property(e => e.ContentType)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Size)
            .IsRequired();
    }
}
