using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Api.Repository.Configurations
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("media");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.CourseId)
                .HasColumnName("course_id")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(200)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.MimeType)
                .HasColumnName("mime_type")
                .HasMaxLength(120)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.StoragePath)
                .HasColumnName("storage_path")
                .HasMaxLength(500)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.SizeBytes)
                .HasColumnName("size_bytes")
                .HasColumnType("bigint(20) unsigned");

            builder.Property(e => e.ChecksumSha256)
                .HasColumnName("checksum_sha256")
                .HasMaxLength(64)
                .IsUnicode(true);

            builder.Property(e => e.UploadedAt)
                .HasColumnName("uploaded_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp()")
                .IsRequired();

            builder.Property(e => e.UploadedBy)
                .HasColumnName("uploaded_by")
                .IsRequired();

            builder.HasOne(m => m.Uploader)
                   .WithMany(u => u.UploadedMedia)
                   .HasForeignKey(m => m.UploadedBy)
                   .HasConstraintName("fk_media_uploader")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(m => m.Course)
                   .WithMany(c => c.Media)
                   .HasForeignKey(m => m.CourseId)
                   .HasConstraintName("fk_media_course")
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
