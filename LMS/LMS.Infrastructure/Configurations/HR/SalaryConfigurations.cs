using LMS.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.HR
{
    public class SalaryConfigurations :
        IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.ToTable("Salaries");

            builder.HasKey(s => s.SalaryId);

            builder.Property(s => s.EmployeeId)
                    .IsRequired();

            builder.Property(s => s.Month)
                    .IsRequired();

            builder.Property(s => s.Value)
                    .HasColumnType("decimal(12,3)")
                    .IsRequired();

            builder.Property(s => s.Year)
                    .IsRequired();

            builder.Property(s => s.IsActive)
                    .IsRequired();

            builder.HasOne(s => s.Employee)
                .WithMany(e => e.Salaries)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
