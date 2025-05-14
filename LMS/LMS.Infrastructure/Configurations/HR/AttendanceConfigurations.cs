using LMS.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.HR
{
    class AttendanceConfigurations: IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendances");

            builder.HasKey(a => a.AttendanceId);

            builder.Property(a => a.EmployeeId)
                    .IsRequired();

            builder.Property(a => a.Date)
                    .IsRequired();

            builder.Property(a => a.TimeIn)
                    .IsRequired();

            builder.Property(a => a.TimeOut)
                    .IsRequired();

            builder.Property(a => a.IsActive)
                    .IsRequired();

            builder.Property(a => a.IsHoliday)
                    .IsRequired(false);

            builder.HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
