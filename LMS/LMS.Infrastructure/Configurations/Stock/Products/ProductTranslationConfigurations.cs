using LMS.Domain.Entities.Stock.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Products
{
    public class ProductTranslationConfigurations :
        IEntityTypeConfiguration<ProductTranslation>
    {

        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.TranslationId);

            builder.Property(p => p.ProductId)
                   .IsRequired();

            builder.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(p => p.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(255);


            builder.Property(p => p.Language)
                    .IsRequired();


            builder.HasOne(pt => pt.Product)
                    .WithMany(p => p.Translations)
                    .HasForeignKey(pt => pt.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
