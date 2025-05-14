using LMS.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.HR
{
    public class PenaltyConfigurations :
        IEntityTypeConfiguration<Penalty>
    {
      public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder.ToTable("Penalties");

            builder.HasKey(p => p.PenaltyId);

            builder.Property(i => i.EmployeeId)
                     .IsRequired();

            builder.Property(i => i.Amount)
                    .HasColumnType("decimal(19,2)")
                    .IsRequired();

            builder.Property(i => i.Reason)
                    .HasMaxLength(265)
                    .IsRequired();

            builder.Property(i => i.DecisionFileUrl)
                    .HasMaxLength(512)
                    .IsRequired();

            builder.Property(i => i.Date)
                    .IsRequired();

            builder.Property(p => p.IsActive)
                    .IsRequired();


            builder.HasOne(p => p.Employee)
                .WithMany(e => e.Penalties)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
