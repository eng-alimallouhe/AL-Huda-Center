using LMS.Domain.Entities.HR;
using LMS.Domain.Entities.Users;
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
            

            builder.Property(p => p.Amount)
                    .HasColumnType("decimal(12,3)")
                    .IsRequired();
            

            builder.Property(p => p.Reason)
                    .IsRequired()
                    .HasMaxLength(256);


            builder.Property(p => p.Date)
                    .IsRequired();
            

            builder.Property(p => p.IsActive)
                    .IsRequired();



            builder.HasOne<Employee>()
                .WithMany(e => e.Penalties)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
