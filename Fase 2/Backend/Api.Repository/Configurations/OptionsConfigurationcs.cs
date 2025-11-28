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
    public class OptionsConfiguration : IEntityTypeConfiguration<Options>
    {
        public void Configure(EntityTypeBuilder<Options> builder)
        {
            builder.ToTable("options");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.QuestionId)
                .HasColumnName("question_id")
                .IsRequired();

            builder.Property(e => e.Text)
                .HasColumnName("text")
                .HasColumnType("text")
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.IsCorrect)
                .HasColumnName("is_correct")
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasOne(o => o.Question)
                  .WithMany(q => q.Options)
                  .HasForeignKey(o => o.QuestionId)
                  .HasConstraintName("fk_option_question")
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Answers)
                   .WithOne(a => a.Option)
                   .HasForeignKey(a => a.OptionId)
                   .HasConstraintName("fk_ans_option")
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
