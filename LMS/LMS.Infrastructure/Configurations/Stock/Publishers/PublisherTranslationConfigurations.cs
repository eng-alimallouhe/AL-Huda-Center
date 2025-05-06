using LMS.Domain.Entities.Stock.Publishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock
{
    public class PublisherTranslationConfigurations :
        IEntityTypeConfiguration<PublisherTranslation>
    {
  
        public void Configure(EntityTypeBuilder<PublisherTranslation> builder)
        {
            builder.ToTable("PublishersTranslations");


            builder.HasKey(pt => pt.TranslationId);


            builder.Property(p => p.PublisherName)
                    .IsRequired()
                    .HasMaxLength(60);


            builder.Property(p => p.Language)
                    .IsRequired();


            builder.Property(p => p.PublisherDescription)
                    .IsRequired()
                    .HasMaxLength(255);


            builder.HasOne(pt => pt.Publisher)
                    .WithMany(p => p.Translations)
                    .HasForeignKey(pt => pt.PublisherId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
        }
    }
}
