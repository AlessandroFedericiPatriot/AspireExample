﻿// <auto-generated />
using System;
using AspireExample.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AspireExample.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AspireExampleDbContext))]
    partial class AspireExampleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AspireExample.Domain.FileDigest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("Id"), 1L, null, null, null, null, null);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Details")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Summary")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UploadId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UploadId");

                    b.ToTable("FileDigests", "file_processor");
                });

            modelBuilder.Entity("AspireExample.Domain.FileUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("Id"), 1L, null, null, null, null, null);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("FileUploads", "file_processor");
                });

            modelBuilder.Entity("AspireExample.Domain.FileDigest", b =>
                {
                    b.HasOne("AspireExample.Domain.FileUpload", null)
                        .WithMany()
                        .HasForeignKey("UploadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
