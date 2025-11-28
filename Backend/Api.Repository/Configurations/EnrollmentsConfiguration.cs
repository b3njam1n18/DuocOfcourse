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
    public class EnrollmentsConfiguration : IEntityTypeConfiguration<Enrollments>
    {
        public void Configure(EntityTypeBuilder<Enrollments> builder)
        {
            builder.ToTable("enrollments");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.StudentId)
                .HasColumnName("student_id")
                .IsRequired();

            builder.Property(e => e.CourseId)
                .HasColumnName("course_id")
                .IsRequired();

            builder.Property(e => e.EnrolledAt)
                .HasColumnName("enrolled_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp()")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasColumnType("enum('IN_PROGRESS','COMPLETED','WITHDRAWN')")
                .HasDefaultValue("IN_PROGRESS")
                .IsUnicode(true)
                .IsRequired();

            builder.HasOne(e => e.Student)
                  .WithMany(u => u.Enrollments)
                  .HasForeignKey(e => e.StudentId)
                  .HasConstraintName("fk_enr_student")
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Course)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(e => e.CourseId)
                   .HasConstraintName("fk_enr_course")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Certificate)
                   .WithOne(c => c.Enrollment)
                   .HasForeignKey<Certificates>(c => c.EnrollmentId)
                   .HasConstraintName("fk_cert_enr")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.CourseProgress)
                   .WithOne(cp => cp.Enrollment)
                   .HasForeignKey(cp => cp.EnrollmentId)
                   .HasConstraintName("fk_progress_enrollment")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.LessonProgress)
                   .WithOne(lp => lp.Enrollment)
                   .HasForeignKey(lp => lp.EnrollmentId)
                   .HasConstraintName("fk_lessonprog_enr")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
