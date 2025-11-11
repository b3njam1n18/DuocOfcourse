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
    public class AttemptsConfiguration : IEntityTypeConfiguration<Attempts>
    {
        public void Configure(EntityTypeBuilder<Attempts> builder)
        {
            builder.ToTable("attempts");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.EvaluationId)
                .HasColumnName("evaluation_id")
                .IsRequired();

            builder.Property(e => e.StudentId)
                .HasColumnName("student_id")
                .IsRequired();

            builder.Property(e => e.StartedAt)
                .HasColumnName("started_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp()")
                .IsRequired();

            builder.Property(e => e.SubmittedAt)
                .HasColumnName("submitted_at")
                .HasColumnType("datetime");

            builder.Property(e => e.Score)
                .HasColumnName("score")
                .HasColumnType("decimal(8,2)");

            builder.HasOne(a => a.Evaluation)
                   .WithMany(e => e.Attempts)
                   .HasForeignKey(a => a.EvaluationId)
                   .HasConstraintName("fk_att_eval")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Student)
                   .WithMany(u => u.Attempts)
                   .HasForeignKey(a => a.StudentId)
                   .HasConstraintName("fk_att_student")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
