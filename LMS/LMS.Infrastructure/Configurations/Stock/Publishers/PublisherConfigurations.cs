using LMS.Domain.Entities.Stock.Publishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Publishers
{
    public class PublisherConfigurations :
        IEntityTypeConfiguration<Publisher>
    {
  
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publishers");

            builder.HasKey(p => p.PublisherId);
            
            builder.Property(p => p.IsActive)
                    .IsRequired();

            builder.Property(p => p.CreatedAt)
                    .IsRequired();

            builder.Property(p => p.UpdatedAt)
                    .IsRequired();

            builder.HasMany(p => p.Books)
                    .WithMany(b => b.Publishers)
                    .UsingEntity(j => j.ToTable("BooksPublishers"));
        }
    }
}
