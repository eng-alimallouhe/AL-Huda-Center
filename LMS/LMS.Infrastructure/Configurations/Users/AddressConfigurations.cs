using LMS.Domain.Entites.Users;
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
            

            builder.Property(a => a.Governorate)
                    .IsRequired()
                    .HasMaxLength(100);


            builder.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100);


            builder.Property(a => a.StreetName)
                    .IsRequired()
                    .HasMaxLength(100);


            builder.Property(a => a.BuildingName)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(a => a.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(60);


            builder.HasOne<User>()
                    .WithMany(c => c.Addresses)
                    .HasForeignKey(a => a.UserId)
                    .IsRequired(false);
        }
    }
}
