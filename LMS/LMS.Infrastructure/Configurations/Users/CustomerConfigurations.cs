using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    public class CustomerConfigurations :
        IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.Property(c => c.Points)
                    .HasColumnType("Decimal(9,2)")
                    .IsRequired();
            
            builder.HasOne(c => c.Level)
                    .WithMany()
                    .HasForeignKey(c => c.LevelId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
        }
    }
}
