// Api.Repository/Configurations/UsersConfiguration.cs
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Repository.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.RoleId)
                .HasColumnName("role_id")
                .IsRequired();


            builder.Property(e => e.CareerId)
            .HasColumnName("category_id");

            builder.Property(e => e.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(80)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.MiddleName)
                .HasColumnName("middle_name")
                .HasMaxLength(80)
                .IsUnicode(true);

            builder.Property(e => e.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(80)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.SecondLastName)
                .HasColumnName("second_last_name")
                .HasMaxLength(80)
                .IsUnicode(true);

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(160)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime");

            builder.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at")
                .HasColumnType("datetime");

            builder.HasOne(u => u.Role)
                   .WithMany(r => r.Users)
                   .HasForeignKey(u => u.RoleId)
                   .HasConstraintName("fk_users_role")
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(u => u.Career)
                   .WithMany()
                   .HasForeignKey(u => u.CareerId)
                   .HasConstraintName("fk_users_career")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.PasswordResetTokens)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("fk_password_reset_user");

            builder.HasMany(u => u.AuthCredentials)
                   .WithOne(ac => ac.User)
                   .HasForeignKey(ac => ac.UserId);

            builder.HasMany(u => u.Notifications)
                   .WithOne(n => n.User)
                   .HasForeignKey(n => n.UserId);

            builder.HasMany(u => u.Attempts)
                   .WithOne(a => a.Student)
                   .HasForeignKey(a => a.StudentId);

            builder.HasMany(u => u.Enrollments)
                   .WithOne(e => e.Student)
                   .HasForeignKey(e => e.StudentId);

            builder.HasMany(u => u.CoursesTaught)
                   .WithOne(c => c.Teacher)
                   .HasForeignKey(c => c.TeacherId);

            builder.HasMany(u => u.UploadedMedia)
                   .WithOne(m => m.Uploader)
                   .HasForeignKey(m => m.UploadedBy);
        }
    }
}
