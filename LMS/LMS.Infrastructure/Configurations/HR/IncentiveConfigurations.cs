using LMS.Domain.Entities.HR;
using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.EmployeesManagement
{
    public class IncentiveConfigurations :
        IEntityTypeConfiguration<Incentive>
    {
        public void Configure(EntityTypeBuilder<Incentive> builder)
        {
            builder.ToTable("Incentives");


            builder.HasKey(i => i.IncentiveId);


            builder.Property(i => i.Amount)
                    .HasColumnType("decimal(12,3)")
                    .IsRequired();


            builder.Property(i => i.Reason)
                    .HasMaxLength(256)
                    .IsRequired();


            builder.Property(i => i.DecisionFileUrl)
                    .HasMaxLength(512)
                    .IsRequired();


            builder.Property(i => i.Date)
                    .IsRequired();


            builder.Property(i => i.IsDisbursed)
                    .IsRequired();


            builder.Property(i => i.IsActive)
                    .IsRequired();



            builder.HasOne<Employee>()
                .WithMany(e => e.Incentives)
                .HasForeignKey(i => i.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
