using LMS.Domain.Entites.Stock.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock
{
    public class GenreTranslationConfigurations :
        IEntityTypeConfiguration<GenreTranslation>
    {
        public void Configure(EntityTypeBuilder<GenreTranslation> builder)
        {
            builder.ToTable("Genres");

            
            builder.HasKey(g => g.TranslationId);

            
            builder.Property(g => g.GenreName)
                    .IsRequired()
                    .HasMaxLength(60);

            
            builder.Property(g => g.GenreDescription)
                    .IsRequired()
                    .HasMaxLength(512);


            builder.Property(g => g.Language)
                    .IsRequired();


            builder.HasOne(gt => gt.Genre)
                    .WithMany(g => g.Translations)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
