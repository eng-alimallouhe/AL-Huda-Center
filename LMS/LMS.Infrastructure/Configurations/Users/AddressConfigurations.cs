using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    public class AddressConfigurations :
        IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.AddressId);
           
            builder.Property(a => a.Latitude)
                    .IsRequired()
                    .HasMaxLength(60);
            
            builder.Property(a => a.Longitude)
                    .IsRequired()
                    .HasMaxLength(60);
            
            builder.Property(a => a.GZipCode)
                    .IsRequired()
                    .HasMaxLength(60);
            
            builder.Property(a => a.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.HasOne<Customer>()
                    .WithMany(c => c.Addresses)
                    .HasForeignKey(a => a.CustomerId)
                    .IsRequired(false);
        }
    }
}
