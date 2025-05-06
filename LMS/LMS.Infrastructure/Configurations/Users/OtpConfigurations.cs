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


            builder.HasKey(x => x.OtpCodeId);


            builder.Property(x => x.HashedValue)
                    .HasMaxLength(60)
                    .IsRequired();
            

            builder.Property(x => x.ExpiredAt)
                    .IsRequired();
            

            builder.Property(x => x.FailedAttempts)
                    .IsRequired();


            builder.Property(x => x.UserId)
                    .IsRequired();
            

            builder.HasOne<User>()
                    .WithMany(u => u.OtpCodes)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
