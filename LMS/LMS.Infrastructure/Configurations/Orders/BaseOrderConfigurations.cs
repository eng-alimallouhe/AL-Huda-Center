using LMS.Domain.Entities.Orders;
using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Orders
{
    public class BaseOrderConfigurations :
        IEntityTypeConfiguration<BaseOrder>
    {
        public void Configure(EntityTypeBuilder<BaseOrder> builder)
        {
            builder.ToTable("BaseOrders");

            builder.HasKey(bo => bo.OrderId);

            builder.Property(bo => bo.CustomerId)
                    .IsRequired();

            builder.Property(bo => bo.EmployeeId)
                    .IsRequired(false);


            builder.Property(bo => bo.Status)
                    .IsRequired();

            builder.Property(bo => bo.DeliveryMethod)
                    .IsRequired();

            builder.Property(bo => bo.PaymentStatus)
                    .IsRequired();

            builder.Property(bo => bo.Cost)
                    .HasColumnType("Decimal(19,2)")
                    .IsRequired();

            builder.Property(bo => bo.IsActive)
                    .IsRequired();

            builder.Property(bo => bo.CreatedAt)
                    .IsRequired();

            builder.Property(bo => bo.UpdatedAt)
                    .IsRequired();


            builder.HasOne(bo => bo.Department)
                    .WithMany()
                    .HasForeignKey(bo => bo.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(bo => bo.Employee)
                    .WithMany(e => e.Orders)
                    .HasForeignKey( bo => bo.EmployeeId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bo => bo.Customer)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(bo => bo.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
