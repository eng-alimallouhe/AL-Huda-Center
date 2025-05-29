using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    public class DepartmentResponsibilityConfigurations :
        IEntityTypeConfiguration<DepartmentResponsibility>
    {
        public void Configure(EntityTypeBuilder<DepartmentResponsibility> builder)
        {
            builder.ToTable("DepartmentResponsibilies");

            builder.HasKey(dr => dr.DepartmentResponsibilityId);

            builder.HasOne(dr => dr.Department)
                .WithOne(d => d.Responsibility)
                .HasForeignKey<DepartmentResponsibility>(dr => dr.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
