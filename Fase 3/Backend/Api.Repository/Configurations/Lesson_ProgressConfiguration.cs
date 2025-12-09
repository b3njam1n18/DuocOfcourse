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
    public class Lesson_ProgressConfiguration : IEntityTypeConfiguration<Lesson_Progress>
    {
        public void Configure(EntityTypeBuilder<Lesson_Progress> builder)
        {
            builder.ToTable("lesson_progress");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.EnrollmentId)
                .HasColumnName("enrollment_id")
                .IsRequired();

            builder.Property(e => e.LessonId)
                .HasColumnName("lesson_id")
                .IsRequired();

            builder.Property(e => e.CompletedAt)
                .HasColumnName("completed_at")
                .HasColumnType("datetime");

            builder.HasOne(lp => lp.Enrollment)
                   .WithMany(e => e.LessonProgress)
                   .HasForeignKey(lp => lp.EnrollmentId)
                   .HasConstraintName("fk_lessonprog_enr")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(lp => lp.Lesson)
                   .WithMany(l => l.LessonProgress)
                   .HasForeignKey(lp => lp.LessonId)
                   .HasConstraintName("fk_lessonprog_lesson")
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
