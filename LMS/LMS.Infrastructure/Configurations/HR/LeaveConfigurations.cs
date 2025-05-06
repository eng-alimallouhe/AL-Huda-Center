using LMS.Domain.Entites.HR;
using LMS.Domain.Entites.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.HR
{
    public class LeaveConfigurations :
        IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.ToTable("Leaves");


            builder.HasKey(l => l.LeaveId);
            

            builder.Property(l => l.StartDate)
                    .IsRequired();
            

            builder.Property(l => l.EndDate)
                    .IsRequired();
            

            builder.Property(l => l.LeaveType)
                    .IsRequired();


            builder.Property(l => l.Reason)
                   .HasMaxLength(512)
                   .IsRequired();


            builder.Property(l => l.IsApproved)
                    .IsRequired();

            
            builder.Property(l => l.IsCompensatory)
                    .IsRequired();


            builder.Property(l => l.IsActive)
                    .IsRequired();

            

            builder.HasOne<Employee>()
                .WithMany(e => e.Leaves)
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
