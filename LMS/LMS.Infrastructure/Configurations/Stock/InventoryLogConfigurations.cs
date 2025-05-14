using LMS.Domain.Entities.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock
{
    public class InventoryLogConfigurations :
        IEntityTypeConfiguration<InventoryLog>
    {
        public void Configure(EntityTypeBuilder<InventoryLog> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(i => i.InventoryLogId);

            builder.Property(i => i.LogDate)
                    .IsRequired();

            builder.Property(i => i.ChangeType)
                    .IsRequired();

            builder.HasOne(i => i.Product)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(i => i.ProductId)
                    .IsRequired();
        }
    }
}
