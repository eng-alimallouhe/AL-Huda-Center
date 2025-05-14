using LMS.Domain.Entities.Stock.Publishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Publishers
{
    public class PublisherTranslationConfigurations :
        IEntityTypeConfiguration<PublisherTranslation>
    {
  
        public void Configure(EntityTypeBuilder<PublisherTranslation> builder)
        {
            builder.ToTable("PublisherTranslations");

            builder.HasKey(a => a.TranslationId);

            builder.Property(at => at.PublisherId)
                    .IsRequired();

            builder.Property(at => at.Language)
                    .IsRequired();

            builder.Property(a => a.PublisherName)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(a => a.PublisherDescription)
                    .IsRequired()
                    .HasMaxLength(512);

            builder.HasOne(at => at.Publisher)
                    .WithMany(a => a.Translations)
                    .HasForeignKey(at => at.PublisherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
