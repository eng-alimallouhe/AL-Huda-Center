using LMS.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class CartConfigurations :
        IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.CartId);

            builder.Property(c => c.CustomerId)
                    .IsRequired();

            builder.Property(c => c.IsCheckedOut)
                    .IsRequired();

            builder.Property(c => c.CreatedAt)
                    .IsRequired();

            builder.Property(c => c.UpdatedAt)
                    .IsRequired();

            builder.HasOne(c => c.Customer)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(c => c.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
