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
    public class CoursesConfiguration : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.ToTable("courses");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(160)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsUnicode(true);

            builder.Property(e => e.TeacherId)
                .HasColumnName("teacher_id")
                .IsRequired();

            builder.Property(e => e.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            builder.Property(e => e.StartsAt)
                .HasColumnName("starts_at")
                .HasColumnType("datetime");

            builder.Property(e => e.EndsAt)
                .HasColumnName("ends_at")
                .HasColumnType("datetime");

            builder.Property(e => e.IsPublished)
                .HasColumnName("is_published")
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

            builder.HasOne(c => c.Category)
                   .WithMany(cat => cat.Courses)
                   .HasForeignKey(c => c.CategoryId)
                   .HasConstraintName("fk_course_category")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
