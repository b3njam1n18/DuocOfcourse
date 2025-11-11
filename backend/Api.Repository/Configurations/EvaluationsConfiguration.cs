using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Api.Repository.Configurations
{
    public class EvaluationsConfiguration : IEntityTypeConfiguration<Evaluations>
    {
        public void Configure(EntityTypeBuilder<Evaluations> builder)
        {
            builder.ToTable("evaluations");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.CourseId)
                .HasColumnName("course_id")
                .IsRequired();

            builder.Property(e => e.LessonId)
                .HasColumnName("lesson_id");

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(160)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsUnicode(true);

            builder.Property(e => e.DueAt)
                .HasColumnName("due_at")
                .HasColumnType("datetime");

            builder.Property(e => e.Type)
                .HasColumnName("type")
                .HasColumnType("enum('QUIZ','TASK','EXAM')")
                .HasDefaultValue("QUIZ")
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.PassThreshold)
                .HasColumnName("pass_threshold")
                .HasColumnType("decimal(5,4)")
                .HasDefaultValue(0.6000m)
                .IsRequired();

            builder.Property(e => e.IsFinalExam)
                .HasColumnName("is_final_exam")
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime");

            builder.HasOne(e => e.Course)
                   .WithMany(c => c.Evaluations)
                   .HasForeignKey(e => e.CourseId)
                   .HasConstraintName("fk_eval_course")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Lesson)
                   .WithMany(l => l.Evaluations)
                   .HasForeignKey(e => e.LessonId)
                   .HasConstraintName("fk_eval_lesson")
                   .OnDelete(DeleteBehavior.SetNull);

     
            builder.HasMany(e => e.Questions)
                   .WithOne(q => q.Evaluation)
                   .HasForeignKey(q => q.EvaluationId)
                   .HasConstraintName("fk_question_eval")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Attempts)
                   .WithOne(a => a.Evaluation)
                   .HasForeignKey(a => a.EvaluationId)
                   .HasConstraintName("fk_att_eval")
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
