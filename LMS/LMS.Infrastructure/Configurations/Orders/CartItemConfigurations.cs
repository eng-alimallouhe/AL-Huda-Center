using LMS.Domain.Entities.Orders;
using LMS.Domain.Entities.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class CartItemConfigurations :
        IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(ci => ci.CartItemId);

            builder.Property(ci => ci.CartId)
                    .IsRequired();

            builder.Property(ci => ci.ProductId)
                    .IsRequired();

            builder.Property(ci => ci.DiscounttId)
                    .IsRequired(false);

            builder.Property(ci => ci.Quantity)
                    .IsRequired();

            builder.Property(ci => ci.UnitPrice)
                    .HasColumnType("decimal(19, 3)");
           
            
            builder.Property(ci => ci.CreatedAt)
                    .IsRequired();
            
            builder.Property(ci => ci.UpdatedAt)
                    .IsRequired();
            
            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);
            
            builder.HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Discount)
                    .WithOne()
                    .HasForeignKey<CartItem>(ci => ci.DiscounttId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
