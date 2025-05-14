using LMS.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class OrderItemConfigurations :
        IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.OrderItemId);
            
            builder.Property(oi => oi.OrderItemId)
                    .ValueGeneratedOnAdd();
            
            builder.Property(oi => oi.Quantity)
                    .IsRequired();
            
            builder.Property(oi => oi.UnitPrice)
                    .HasColumnType("decimal(7, 2)");
            
            builder.Property(oi => oi.Discount)
                    .HasColumnType("decimal(7, 2)")
                    .IsRequired();
            
            builder.Property(oi => oi.TotalPrice)
                    .HasColumnType("decimal(7, 2)");
            
            builder.Property(oi => oi.IsActive)
                    .IsRequired();
            
            builder.Property(oi => oi.CreatedAt)
                    .IsRequired();
            
            builder.Property(oi => oi.UpdatedAt)
                    .IsRequired();
            
            builder.HasOne(oi => oi.SellOrder)
                .WithMany(so => so.OrderItems)
                .HasForeignKey(oi => oi.SellOrderId);
            
            builder.HasOne(oi => oi.Product)
                    .WithMany()
                    .HasForeignKey(oi => oi.ProductId);
        }
    }
}
