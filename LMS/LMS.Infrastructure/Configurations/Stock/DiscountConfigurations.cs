using LMS.Domain.Entities.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock
{
    public class DiscountConfigurations :
        IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");

            builder.HasKey(d => d.DiscountId);
            
            builder.Property(d => d.DiscountPercentage)
                    .HasColumnType("Decimal(9,2)")
                    .IsRequired();
            
            builder.Property(d => d.StartDate)
                    .IsRequired();
            
            builder.Property(d => d.EndDate)
                    .IsRequired();
            
            builder.Property(d => d.IsActive)
                    .IsRequired();
            
            builder.Property(d => d.CreatedAt)
                    .IsRequired();
            
            builder.Property(d => d.UpdatedAt)
                    .IsRequired();
            
            builder.HasOne(d => d.Product)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
