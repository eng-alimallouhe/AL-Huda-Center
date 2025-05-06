using LMS.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class PrintOrderConfigurations :
        IEntityTypeConfiguration<PrintingOrder>
    {
        public void Configure(EntityTypeBuilder<PrintingOrder> builder)
        {
            builder.ToTable("PrintOrders");

            builder.Property(po => po.StartPage)
                    .IsRequired();
            
            builder.Property(po => po.EndPage)
                    .IsRequired();
            
            builder.Property(po => po.CopiesCount)
                    .IsRequired();
            
            builder.Property(po => po.CopyCost)
                    .HasColumnType("decimal(12, 3)");
    
            builder.Property(po => po.FileUrl)
                    .IsRequired();
            
            builder.Property(po => po.FileName)
                    .IsRequired();
        }
    }
}
