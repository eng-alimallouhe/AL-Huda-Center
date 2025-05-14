using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    public class EmployeeDepartmentConfigurations :
        IEntityTypeConfiguration<EmployeeDepartment>
    {
        public void Configure(EntityTypeBuilder<EmployeeDepartment> builder)
        {
            builder.ToTable("EmployeesDepartments");

            builder.HasKey(ed => ed.EmployeeDepartmentId);

            builder.Property(ed => ed.DepartmentId)
                    .IsRequired();

            builder.Property(ed => ed.EmployeeId)
                    .IsRequired();

            builder.Property(ed => ed.AppointmentDecisionUrl)
                    .IsRequired()
                    .HasMaxLength(512);

            builder.Property(ed => ed.StartDate)
                    .IsRequired();

            builder.Property(ed => ed.EndDate)
                    .IsRequired(false);

            builder.Property(ed => ed.IsActive)
                    .IsRequired();

            builder.HasOne(ed => ed.Employee)
                    .WithMany(e => e.EmployeeDepartments)
                    .HasForeignKey(ed => ed.EmployeeId)
                    .IsRequired();

            builder.HasOne(ed => ed.Department)
                    .WithMany(d => d.EmployeeDepartments)
                    .HasForeignKey(ed => ed.DepartmentId)
                    .IsRequired();
        }
    }
}
