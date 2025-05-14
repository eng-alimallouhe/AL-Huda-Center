using LMS.Domain.Entities.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Financial
{
    public class FinancialRevenueConfigurations :
        IEntityTypeConfiguration<FinancialRevenue>
    {
        public void Configure(EntityTypeBuilder<FinancialRevenue> builder)
        {
            builder.ToTable("Financials");

            builder.HasKey(fr => fr.FinancialRevenueId);

            builder.Property(fr => fr.CustomerId)
                    .IsRequired();

            builder.Property(fr => fr.EmployeeId)
                    .IsRequired();

            builder.Property(fr => fr.Amount)
                    .HasColumnType("Decimal(19,2)")
                    .IsRequired();
            
            builder.Property(fr => fr.IsActive)
                    .IsRequired();

            builder.Property(fr => fr.PaymentMethod)
                    .IsRequired();

            builder.Property(fr => fr.PaymentStatus)
                    .IsRequired();

            builder.Property(fr => fr.Service)
                    .IsRequired();

            builder.Property(l => l.CreatedAt)
                    .IsRequired();
            
            builder.Property(l => l.UpdatedAt)
                    .IsRequired();

            builder.HasOne(fr => fr.Customer)
                .WithMany(c => c.FinancialRevenues)
                .HasForeignKey(fr => fr.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(fr => fr.Employee)
                .WithMany(e => e.FinancialRevenues)
                .HasForeignKey(fr => fr.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
