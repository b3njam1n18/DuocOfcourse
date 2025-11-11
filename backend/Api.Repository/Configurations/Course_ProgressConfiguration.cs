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
    public class Course_ProgressConfiguration : IEntityTypeConfiguration<Course_Progress>
    {
        public void Configure(EntityTypeBuilder<Course_Progress> builder)
        {
            builder.ToTable("course_progress");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.EnrollmentId)
                .HasColumnName("enrollment_id")
                .IsRequired();

            builder.Property(e => e.Percentage)
                .HasColumnName("percentage")
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0.00m)
                .IsRequired();

            builder.Property(e => e.LastUpdateAt)
                .HasColumnName("last_update_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp()")
                .IsRequired();

            builder.HasOne(cp => cp.Enrollment)
                   .WithMany(e => e.CourseProgress)
                   .HasForeignKey(cp => cp.EnrollmentId)
                   .HasConstraintName("fk_progress_enrollment")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cp => cp.Course)
                   .WithMany(c => c.CourseProgress)
                   .HasForeignKey(cp => cp.Id)
                   .HasConstraintName("fk_progress_course")
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
