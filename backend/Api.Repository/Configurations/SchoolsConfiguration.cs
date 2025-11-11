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
    public class SchoolsConfiguration : IEntityTypeConfiguration<Schools>
    {
        public void Configure(EntityTypeBuilder<Schools> builder)
        {
            
            builder.ToTable("Schools");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd() 
                   .IsRequired();

            builder.Property(e => e.Name)
                   .HasColumnName("name")
                   .HasMaxLength(120)
                   .IsUnicode(false)
                   .IsRequired();

            builder.HasMany(s => s.CourseCategories)
                  .WithOne(c => c.School)
                  .HasForeignKey(c => c.SchoolId)
                  .HasConstraintName("fk_course_school")
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
