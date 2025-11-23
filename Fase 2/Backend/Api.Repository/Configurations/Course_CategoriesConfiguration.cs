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
    public class Course_CategoriesConfiguration : IEntityTypeConfiguration<Course_Categories>
    {
        public void Configure(EntityTypeBuilder<Course_Categories> builder)
        {
            builder.ToTable("course_categories");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.SchoolId)
                .HasColumnName("school_id")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(120)
                .IsUnicode(true)
                .IsRequired();

            builder.HasMany(cc => cc.Courses)
                   .WithOne(c => c.Category)
                   .HasForeignKey(c => c.CategoryId)
                   .HasConstraintName("fk_course_cat")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.School)
                .WithMany(s => s.CourseCategories)
                .HasForeignKey(e => e.SchoolId)
                .HasConstraintName("fk_school_course_categories")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
