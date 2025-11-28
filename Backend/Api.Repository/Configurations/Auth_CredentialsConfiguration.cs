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
    public class Auth_CredentialsConfiguration : IEntityTypeConfiguration<Auth_Credentials>
    {
        public void Configure(EntityTypeBuilder<Auth_Credentials> builder)
        {
            builder.ToTable("auth_credentials");

            builder.HasKey(e => e.UserId);

            builder.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(e => e.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.EmailVerified)
                .HasColumnName("email_verified")
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(e => e.LastLoginAt)
                .HasColumnName("last_login_at")
                .HasColumnType("datetime");

            builder.Property(e => e.PasswordUpdatedAt)
                .HasColumnName("password_updated_at")
                .HasColumnType("datetime");

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp()");

            builder.HasOne(ac => ac.User)
                   .WithMany(u => u.AuthCredentials)
                   .HasForeignKey(ac => ac.UserId)
                   .HasConstraintName("fk_auth_user")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
