using LMS.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.HR
{
    public class LeaveBalanceConfigurations :
        IEntityTypeConfiguration<LeaveBalance>
    {
        public void Configure(EntityTypeBuilder<LeaveBalance> builder)
        {
            builder.ToTable("LeavesBalances");

            builder.HasKey(lb => lb.LeaveBalanceId);

            builder.Property(lb => lb.RemainBalance)
                    .IsRequired();

            builder.Property(lb => lb.TotalBalance)
                    .IsRequired();

            builder.Property(builder => builder.BaseBalance)
                    .IsRequired();

            builder.Property(lb => lb.RoundedBalance)
                    .IsRequired();


            builder.Property(lb => lb.Year)
                    .IsRequired();

            builder.HasOne(lb => lb.Employee)
                .WithOne(e => e.LeaveBalance)
                .HasForeignKey<LeaveBalance>(lb => lb.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
