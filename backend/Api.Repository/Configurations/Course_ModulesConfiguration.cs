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
    public class Course_ModulesConfiguration : IEntityTypeConfiguration<Course_Modules>
    {
        public void Configure(EntityTypeBuilder<Course_Modules> builder)
        {
            builder.ToTable("course_modules");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.CourseId)
                .HasColumnName("course_id")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(160)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.Position)
                .HasColumnName("position")
                .HasColumnType("int(10) unsigned")
                .HasDefaultValue(1)
                .IsRequired();

            builder.HasOne(cm => cm.Course)
                   .WithMany(c => c.Modules)
                   .HasForeignKey(cm => cm.CourseId)
                   .HasConstraintName("fk_module_course")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cm => cm.Lessons)
                   .WithOne(l => l.Module)
                   .HasForeignKey(l => l.ModuleId)
                   .HasConstraintName("fk_lesson_module")
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
