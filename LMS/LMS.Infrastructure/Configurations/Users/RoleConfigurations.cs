using LMS.Domain.Entites.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    public class RoleConfigurations :
        IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");


            builder.HasKey(r => r.RoleId);


            builder.Property(r => r.RoleType)
                    .IsRequired()
                    .HasMaxLength(60);
            

            builder.Property(r => r.IsActive)
                    .IsRequired();


            builder.Property(r => r.CreatedAt)
                    .IsRequired();


            builder.Property(r => r.UpdatedAt)
                    .IsRequired();
        }
    }
}
