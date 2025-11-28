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
    public class CertificatesConfiguration : IEntityTypeConfiguration<Certificates>
    {
        public void Configure(EntityTypeBuilder<Certificates> builder)
        {
            builder.ToTable("certificates");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.EnrollmentId)
                .HasColumnName("enrollment_id")
                .IsRequired();

            builder.Property(e => e.PdfPath)
                .HasColumnName("pdf_path")
                .HasMaxLength(500)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.IssuedAt)
                .HasColumnName("issued_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp()")
                .IsRequired();

            builder.Property(e => e.VerificationCode)
                .HasColumnName("verification_code")
                .HasColumnType("char(16)")
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.GradeSummary)
                .HasColumnName("grade_summary")
                .HasColumnType("longtext")
                .IsUnicode(true);

            builder.HasOne(c => c.Enrollment)
                   .WithOne(e => e.Certificate)
                   .HasForeignKey<Certificates>(c => c.EnrollmentId)
                   .HasConstraintName("fk_cert_enr")
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
