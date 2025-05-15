using LMS.Domain.Entities.Financial.Levels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Financial.Levels
{
    public class LevelConfigurations :
        IEntityTypeConfiguration<LoyaltyLevel>
    {
        public void Configure(EntityTypeBuilder<LoyaltyLevel> builder)
        {
            builder.ToTable("Levels");

            builder.HasKey(l => l.LevelId);

            builder.Property(l => l.RequiredPoints)
                    .IsRequired();
            
            builder.Property(l => l.DiscountPercentage)
                    .HasColumnType("Decimal(7,2)")
                    .IsRequired();

            builder.Property(l => l.PointPerDolar)
                   .HasColumnType("Decimal(7,2)")
                   .IsRequired();

            builder.Property(l => l.IsActive)
                    .IsRequired();

            builder.Property(l => l.CreatedAt)
                    .IsRequired();
            
            builder.Property(l => l.UpdatedAt)
                    .IsRequired();
        }
    }
}
