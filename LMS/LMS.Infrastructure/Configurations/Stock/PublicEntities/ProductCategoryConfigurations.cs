using LMS.Domain.Entities.Stock.PublicEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.PublicEntities
{
    public class ProductCategoryConfigurations
        : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductsCategories");

            builder.HasKey(pc => pc.ProductCategoryId);

            builder.Property(pc => pc.ProductId)
                .IsRequired();


            builder.Property(pc => pc.CategoryId)
                .IsRequired();

            builder.HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategoriys)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategoriys)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
