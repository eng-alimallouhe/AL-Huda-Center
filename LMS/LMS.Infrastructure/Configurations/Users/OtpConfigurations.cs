using LMS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Users
{
    public class OtpConfigurations : IEntityTypeConfiguration<OtpCode>
    {
        public void Configure(EntityTypeBuilder<OtpCode> builder)
        {
            builder.ToTable("OtpCodes");

            builder.HasKey(oc => oc.OtpCodeId);

            builder.Property(oc => oc.UserId)
                    .IsRequired();

            builder.Property(oc => oc.HashedValue)
                    .HasMaxLength(60)
                    .IsRequired();
            
            builder.Property(oc => oc.FailedAttempts)
                    .IsRequired();

            builder.Property(oc => oc.CodeType)
                    .IsRequired();

            builder.Property(oc => oc.ExpiredAt)
                    .IsRequired();
                
            builder.HasOne<User>()
                    .WithOne(u => u.OtpCode)
                    .HasForeignKey<OtpCode>(oc => oc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
