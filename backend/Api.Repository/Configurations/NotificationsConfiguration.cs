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
    public class NotificationsConfiguration : IEntityTypeConfiguration<Notifications>
    {
        public void Configure(EntityTypeBuilder<Notifications> builder)
        {
            builder.ToTable("notifications");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(e => e.Kind)
                .HasColumnName("kind")
                .HasColumnType("enum('NEW_COURSE','EXAM_REMINDER','CERT_AVAILABLE')")
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.Payload)
                .HasColumnName("payload")
                .HasColumnType("longtext")
                .IsUnicode(true);

            builder.Property(e => e.Channel)
                .HasColumnName("channel")
                .HasColumnType("enum('EMAIL')")
                .HasDefaultValue("EMAIL")
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasColumnType("enum('PENDING','SENT','ERROR')")
                .HasDefaultValue("PENDING")
                .IsUnicode(true)
                .IsRequired();

            builder.Property(e => e.ScheduledAt)
                .HasColumnName("scheduled_at")
                .HasColumnType("datetime");

            builder.Property(e => e.SentAt)
                .HasColumnName("sent_at")
                .HasColumnType("datetime");

            builder.Property(e => e.ErrorMsg)
                .HasColumnName("error_msg")
                .HasMaxLength(500)
                .IsUnicode(true);

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(n => n.User)
                   .WithMany(u => u.Notifications)
                   .HasForeignKey(n => n.UserId)
                   .HasConstraintName("fk_notif_user")
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
