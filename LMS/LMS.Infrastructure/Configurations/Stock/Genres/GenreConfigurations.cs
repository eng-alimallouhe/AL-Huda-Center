using LMS.Domain.Entities.Stock.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Genres
{
    public class GenreConfigurations :
        IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(g => g.GenreId);

            builder.Property(g => g.IsActive)
                    .IsRequired();
            
            builder.Property(g => g.CreatedAt)
                    .IsRequired();
            
            builder.Property(g => g.UpdatedAt)
                    .IsRequired();
        }
    }
}
