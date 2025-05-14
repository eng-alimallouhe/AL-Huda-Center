using LMS.Domain.Entities.Stock.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Genres
{
    public class GenreTranslationConfigurations :
        IEntityTypeConfiguration<GenreTranslation>
    {
        public void Configure(EntityTypeBuilder<GenreTranslation> builder)
        {
            builder.ToTable("GenreTranslations");

            builder.HasKey(a => a.TranslationId);

            builder.Property(at => at.GenreId)
                    .IsRequired();

            builder.Property(at => at.Language)
                    .IsRequired();

            builder.Property(a => a.GenreName)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(a => a.GenreDescription)
                    .IsRequired()
                    .HasMaxLength(512);

            builder.HasOne(at => at.Genre)
                    .WithMany(a => a.Translations)
                    .HasForeignKey(at => at.GenreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
