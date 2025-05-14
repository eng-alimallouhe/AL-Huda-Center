using LMS.Domain.Entities.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Financial
{
    public class PaymentConfigurations :
        IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.Date)
                    .IsRequired();

            builder.Property(p => p.Amount)
                    .HasColumnType("decimal(19, 2)");

            builder.Property(p => p.IsActive)
                    .IsRequired();
        }
    }
}
