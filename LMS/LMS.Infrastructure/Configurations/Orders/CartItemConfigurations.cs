using LMS.Domain.Entites.Orders;
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
            
            builder.Property(ci => ci.CartItemId)
                    .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UnitPrice)
                    .HasColumnType("decimal(19, 2)");
            
            
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
        }
    }
}
