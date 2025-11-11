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
    public class AnswersConfiguration : IEntityTypeConfiguration<Answers>
    {
        public void Configure(EntityTypeBuilder<Answers> builder)
        {
            builder.ToTable("answers");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.AttemptId)
                .HasColumnName("attempt_id")
                .IsRequired();

            builder.Property(e => e.QuestionId)
                .HasColumnName("question_id")
                .IsRequired();

            builder.Property(e => e.OptionId)
                .HasColumnName("option_id");

            builder.Property(e => e.OpenText)
                .HasColumnName("open_text")
                .HasColumnType("text")
                .IsUnicode(true); 

            builder.Property(e => e.IsCorrect)
                .HasColumnName("is_correct");

            builder.Property(e => e.PointsAwarded)
                .HasColumnName("points_awarded")
                .HasColumnType("decimal(6,2)");

            builder.HasOne(a => a.Attempt)
                   .WithMany(at => at.Answers)
                   .HasForeignKey(a => a.AttemptId)
                   .HasConstraintName("fk_ans_attempt")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Question)
                   .WithMany(q => q.Answers)
                   .HasForeignKey(a => a.QuestionId)
                   .HasConstraintName("fk_ans_question")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Option)
                   .WithMany(o => o.Answers)
                   .HasForeignKey(a => a.OptionId)
                   .HasConstraintName("fk_ans_option")
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
