using LMS.Domain.Entities.Stock.Categories;
using LMS.Domain.Entities.Stock.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Categories
{
    public class CategoryTranslationConfigurations :
        IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");

            builder.HasKey(a => a.TranslationId);

            builder.Property(at => at.CategoryId)
                    .IsRequired();

            builder.Property(at => at.Language)
                    .IsRequired();

            builder.Property(a => a.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(a => a.CategoryDescription)
                    .IsRequired()
                    .HasMaxLength(512);

            builder.HasOne(at => at.Category)
                    .WithMany(a => a.Translations)
                    .HasForeignKey(at => at.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
