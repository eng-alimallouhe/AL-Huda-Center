using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    public class EmployeeConfigurations :
        IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.Property(e => e.HireDate)
                    .IsRequired();

            builder.Property(e => e.BaseSalary)
                    .HasColumnType("Decimal(18,2)")
                    .IsRequired();
        }
    }
}
