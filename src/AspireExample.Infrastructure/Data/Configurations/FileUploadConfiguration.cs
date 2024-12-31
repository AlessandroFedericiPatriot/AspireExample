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
    public void Configure(EntityTypeBuilder<FileUpload> builder)
    {
        builder.ToTable("FileUploads");
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .HasConversion(new FileUploadId.EfCoreValueConverter());
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
        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(255);        
    }
}
