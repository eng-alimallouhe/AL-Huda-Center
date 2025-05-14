using LMS.Domain.Entities.Stock.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Authors
{
    public class AuthorTranslationConfigurations :
        IEntityTypeConfiguration<AuthorTranslation>
    {
        public void Configure(EntityTypeBuilder<AuthorTranslation> builder)
        {
            builder.ToTable("AuthorTranslations");

            builder.HasKey(a => a.TranslationId);

            builder.Property(at => at.AuthorId)
                    .IsRequired();

            builder.Property(at => at.Language)
                    .IsRequired();

            builder.Property(a => a.AuthorName)
                    .IsRequired()
                    .HasMaxLength(60);
            
            builder.Property(a => a.AuthorDescription)
                    .IsRequired()
                    .HasMaxLength(512);
            
            builder.HasOne(at => at.Author)
                    .WithMany(a => a.Translations)
                    .HasForeignKey(at => at.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
