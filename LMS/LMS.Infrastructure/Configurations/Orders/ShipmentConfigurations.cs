using LMS.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class ShipmentConfigurations :
        IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("Shipment");

            builder.Property(s => s.AddressId)
                    .IsRequired();
       
            builder.HasOne(dor => dor.Address)
                    .WithOne()
                    .HasForeignKey<Shipment>(s => s.AddressId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
        }
    }
}
