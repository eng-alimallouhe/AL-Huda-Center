using LMS.Domain.Entites.Financial.Levels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Financial
{
    public class LevelTranslationConfigurations :
        IEntityTypeConfiguration<LoyaltyLevelTranslation>
    {
        public void Configure(EntityTypeBuilder<LoyaltyLevelTranslation> builder)
        {
            builder.ToTable("Levels");

            builder.HasKey(lt => lt.TranslationId);


            builder.Property(l => l.LevelName)
                    .IsRequired()
                    .HasMaxLength(100);
            

            builder.Property(l => l.LevelDescription)
                    .IsRequired()
                    .HasMaxLength(255);

            builder.HasOne(lt => lt.Level)
                    .WithMany(l => l.Translations)
                    .HasForeignKey(lt => lt.LevelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
