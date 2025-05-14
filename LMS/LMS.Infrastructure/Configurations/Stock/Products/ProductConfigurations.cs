using LMS.Domain.Entities.Stock.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Products
{
    public class ProductConfigurations :
        IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductPrice)
                    .HasColumnType("Decimal(19,2)")
                    .IsRequired();

            builder.Property(p => p.ProductStock)
                    .IsRequired();

            builder.Property(p => p.IsActive)
                    .IsRequired();

            builder.Property(p => p.CreatedAt)
                    .IsRequired();

            builder.Property(p => p.UpdatedAt)
                    .IsRequired();
        }
    }
}
