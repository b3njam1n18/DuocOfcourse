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
    public class LessonsConfiguration : IEntityTypeConfiguration<Lessons>
    {
        public void Configure(EntityTypeBuilder<Lessons> builder)
        {
            builder.ToTable("lessons");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.ModuleId)
                .HasColumnName("module_id")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(160)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.ContentUrl)
                .HasColumnName("content_url")
                .HasMaxLength(500)
                .IsUnicode(true);

            builder.Property(e => e.DurationMinutes)
                .HasColumnName("duration_minutes")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Position)
                .HasColumnName("position")
                .HasColumnType("int(10)")
                .HasDefaultValue(1)
                .IsRequired();

            builder.HasOne(l => l.Module)
                   .WithMany(m => m.Lessons)
                   .HasForeignKey(l => l.ModuleId)
                   .HasConstraintName("fk_lesson_module")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Evaluations)
                   .WithOne(e => e.Lesson)
                   .HasForeignKey(e => e.LessonId)
                   .HasConstraintName("fk_eval_lesson")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(l => l.LessonProgress)
                   .WithOne(lp => lp.Lesson)
                   .HasForeignKey(lp => lp.LessonId)
                   .HasConstraintName("fk_lessonprog_lesson")
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
