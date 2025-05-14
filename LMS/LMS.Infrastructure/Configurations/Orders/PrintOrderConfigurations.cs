using LMS.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class PrintOrderConfigurations :
        IEntityTypeConfiguration<PrintOrder>
    {
        public void Configure(EntityTypeBuilder<PrintOrder> builder)
        {
            builder.ToTable("PrintOrders");

            builder.Property(po => po.StartPage)
                    .IsRequired();
            
            builder.Property(po => po.EndPage)
                    .IsRequired();
            
            builder.Property(po => po.CopiesCount)
                    .IsRequired();
            
            builder.Property(po => po.CopyCost)
                    .HasColumnType("decimal(7, 2)");
    
            builder.Property(po => po.FileUrl)
                    .HasMaxLength(512)
                    .IsRequired();
            
            builder.Property(po => po.FileName)
                    .HasMaxLength(256)
                    .IsRequired();

        }
    }
}
