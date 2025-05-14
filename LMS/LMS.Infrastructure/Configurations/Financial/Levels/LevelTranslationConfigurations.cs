using LMS.Domain.Entities.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Financial
{
    public class LevelTranslationConfigurations :
        IEntityTypeConfiguration<LoyaltyLevelTransaltion>
    {
        public void Configure(EntityTypeBuilder<LoyaltyLevelTransaltion> builder)
        {
            builder.ToTable("LevelTranslation");

            builder.HasKey(l => l.TranslationId);

            builder.Property(lt => lt.LevelId)
                    .IsRequired();

            builder.Property(lt => lt.Language)
                    .IsRequired();

            builder.Property(l => l.LevelName)
                    .IsRequired()
                    .HasMaxLength(60);
           
            builder.Property(l => l.LevelDescription)
                    .IsRequired()
                    .HasMaxLength(255);


            builder.HasOne(lt => lt.LoyaltyLevel)
                    .WithMany(l => l.Translations)
                    .HasForeignKey(lt => lt.LevelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
