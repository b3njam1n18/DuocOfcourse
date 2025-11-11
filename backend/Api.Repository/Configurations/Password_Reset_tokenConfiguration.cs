using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Api.Repository.Configurations
{
    public class Password_Reset_TokenConfiguration : IEntityTypeConfiguration<Password_Reset_Token>
    {
        public void Configure(EntityTypeBuilder<Password_Reset_Token> builder)
        {
            builder.ToTable("password_reset_tokens");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(e => e.Token)
                .HasColumnName("token")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.ExpiresAt)
                .HasColumnName("expires_at")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.IsUsed)
                .HasColumnName("is_used")
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(false);

            builder.HasOne(e => e.User)
                .WithMany(u => u.PasswordResetTokens)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_password_reset_user");

        }
    }
}
