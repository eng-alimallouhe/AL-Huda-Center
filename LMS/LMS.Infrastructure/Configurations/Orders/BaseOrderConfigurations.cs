using LMS.Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class BaseOrderConfigurations :
        IEntityTypeConfiguration<BaseOrder>
    {
        public void Configure(EntityTypeBuilder<BaseOrder> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.BaseOrderId);

            builder.Property(o => o.CustomerId)
                    .IsRequired();

            builder.Property(o => o.EmployeeId)
                    .IsRequired();

            builder.Property(o => o.DepartmentId)
                    .IsRequired();

            builder.Property(o => o.Status)
                    .IsRequired();

            builder.Property(o => o.DeliveryMethod)
                    .IsRequired();


            builder.Property(o => o.PaymentStatus)
                    .IsRequired();

            builder.Property(o => o.Cost)
                    .HasColumnType("Decimal(19,3)")
                    .IsRequired();

            builder.Property(o => o.IsActive)
                    .IsRequired();

            builder.Property(o => o.CreatedAt)
                    .IsRequired();

            builder.Property(o => o.UpdatedAt)
                    .IsRequired();


            builder.HasOne(o => o.Department)
                    .WithMany(d => d.Orders)
                    .HasForeignKey(o => o.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Employee)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(o => o.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Customer)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
