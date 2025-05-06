using LMS.Domain.Entities.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock
{
    public class PurchaseConfigurations :
        IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases");


            builder.HasKey(p => p.PurchaseId);
           

            builder.Property(p => p.PurchaseDate)
                    .IsRequired();
            

            builder.Property(p => p.TotalAmount)
                    .HasColumnType("Decimal(16,3)")
                    .IsRequired();
            

            builder.Property(p => p.CurrencyCode)
                    .HasMaxLength(3)
                    .IsRequired();


            builder.Property(p => p.PurchaseImgUrl)
                    .HasMaxLength(256)
                    .IsRequired();


            builder.Property(p => p.Notes)
                    .HasMaxLength(512)
                    .IsRequired(false);
            

            builder.Property(p => p.IsActive)
                    .IsRequired();
            

            builder.Property(p => p.CreatedAt)
                    .IsRequired();
            

            builder.Property(p => p.UpdatedAt)
                    .IsRequired();
            
            
            builder.HasOne(p => p.Supplier)
                    .WithMany(s => s.Purchases)
                    .HasForeignKey(p => p.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
