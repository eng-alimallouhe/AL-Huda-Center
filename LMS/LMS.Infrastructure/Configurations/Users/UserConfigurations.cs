using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    public class UserConfigurations :
        IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);


            builder.Property(u => u.FullName)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.HasIndex(u => u.UserName)
                    .IsUnique();

            builder.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.HasIndex(u => u.Email)
                    .IsUnique();


            builder.Property(u => u.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(23);

            builder.HasIndex(u => u.PhoneNumber)
                    .IsUnique();


            builder.Property(u => u.HashedPassword)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(u => u.FailedLoginAttempts)
                    .IsRequired();

            builder.Property(u => u.IsLocked)
                    .IsRequired();

            builder.Property(u => u.IsEmailConfirmed)
                    .IsRequired();

            builder.Property(u => u.IsTwoFactorEnabled)
                    .IsRequired();

            builder.Property(u => u.IsDeleted)
                    .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            builder.Property(u => u.UpdatedAt)
                    .IsRequired();

            builder.Property(u => u.LockedUntil)
                    .IsRequired(false);

            builder.HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .IsRequired();
        }
    }
}
