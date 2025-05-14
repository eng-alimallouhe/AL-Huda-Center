using LMS.Domain.Entities.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock
{
    public class SupplierConfigurations :
        IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(s => s.SupplierId);

            builder.Property(s => s.SupplierName)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(s => s.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(255);

            builder.Property(s => s.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(20);

            builder.Property(s => s.IsActive)
                    .IsRequired();

            builder.Property(p => p.CreatedAt)
                    .IsRequired();

            builder.Property(p => p.UpdatedAt)
                    .IsRequired();
        }
    }
}
