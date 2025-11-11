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
    public class QuestionsConfiguration : IEntityTypeConfiguration<Questions>
    {
        public void Configure(EntityTypeBuilder<Questions> builder)
        {
            builder.ToTable("questions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.EvaluationId)
                .HasColumnName("evaluation_id")
                .IsRequired();

            builder.Property(e => e.Prompt)
                .HasColumnName("prompt")
                .HasColumnType("text")
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.Type)
                .HasColumnName("type")
                .HasColumnType("enum('SINGLE','MULTI','OPEN')")
                .HasDefaultValue("SINGLE")
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.Points)
                .HasColumnName("points")
                .HasColumnType("decimal(6,2)")
                .HasDefaultValue(1.00m)
                .IsRequired();

            builder.Property(e => e.Position)
                .HasColumnName("position")
                .HasColumnType("int(10)")
                .HasDefaultValue(1)
                .IsRequired();

            builder.HasOne(q => q.Evaluation)
                   .WithMany(e => e.Questions)
                   .HasForeignKey(q => q.EvaluationId)
                   .HasConstraintName("fk_question_eval")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.Options)
                   .WithOne(o => o.Question)
                   .HasForeignKey(o => o.QuestionId)
                   .HasConstraintName("fk_option_question")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.Answers)
                   .WithOne(a => a.Question)
                   .HasForeignKey(a => a.QuestionId)
                   .HasConstraintName("fk_ans_question")
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
