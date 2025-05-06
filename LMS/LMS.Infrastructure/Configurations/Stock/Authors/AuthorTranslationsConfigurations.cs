using LMS.Domain.Entities.Stock.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock
{
    public class AuthorTranslationsConfigurations :
        IEntityTypeConfiguration<AuthorTranslation>
    {
        public void Configure(EntityTypeBuilder<AuthorTranslation> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(a => a.TranslationId);

            builder.Property(a => a.AuthorName)
                    .IsRequired()
                    .HasMaxLength(100);
            
            builder.Property(a => a.AuthorDescription)
                    .IsRequired()
                    .HasMaxLength(512);
          

            builder.HasOne(at => at.Author)
                    .WithMany(a => a.Translations)
                    .HasForeignKey(a => a.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
