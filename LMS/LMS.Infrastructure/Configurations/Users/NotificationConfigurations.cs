using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    internal class NotificationConfigurations : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.NotificationId);

            builder.Property(n => n.UserId)
                    .IsRequired();

            builder.Property(n => n.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            
            builder.Property(n => n.Message)
                    .IsRequired()
                    .HasMaxLength(255);

            builder.Property(n => n.CreatedAt)
                    .IsRequired();

            builder.Property(n => n.IsRead)
                    .IsRequired();

            builder.Property(n => n.ReadAt)
                    .IsRequired(false);

            builder.Property(n => n.RedirectUrl)
                    .IsRequired(false)
                    .HasMaxLength(255);

            builder.HasOne<User>()
                    .WithMany(u => u.Notifications)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}