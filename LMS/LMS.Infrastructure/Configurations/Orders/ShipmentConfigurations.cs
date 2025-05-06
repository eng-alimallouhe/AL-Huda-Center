using LMS.Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class ShipmentConfigurations :
        IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("Shipments");


            builder.HasKey(s => s.ShipmentId);


            builder.Property(s => s.OrderId)
                    .IsRequired();


            builder.Property(s => s.AddressId)
                    .IsRequired();


            builder.Property(s => s.CustomerId)
                    .IsRequired();


            builder.Property(s => s.EmployeeId)
                    .IsRequired();



            builder.Property(s => s.TrackingNumber)
                    .HasMaxLength(22)
                    .IsRequired();



            builder.Property(s => s.Status)
                    .IsRequired();


            builder.Property(s => s.EstimatedDeliveryDate)
                    .IsRequired(false);


            builder.Property(s => s.DeliveredAt)
                    .IsRequired(false);



            builder.Property(s => s.IsDelivered)
                    .IsRequired();


            builder.Property(s => s.IsActive)
                    .IsRequired();


            builder.HasOne(dor => dor.Address)
                    .WithOne()
                    .HasForeignKey<Shipment>(dor => dor.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Customer)
                    .WithMany(c => c.Shipments)
                    .HasForeignKey(s => s.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Employee)
                    .WithMany(e => e.Shipments)
                    .HasForeignKey(s => s.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
