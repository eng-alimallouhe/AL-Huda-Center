using LMS.Domain.Entities.Stock.PublicEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.PublicEntities
{
    public class PublisherBookConfigurations
        : IEntityTypeConfiguration<PublisherBook>
    {
        public void Configure(EntityTypeBuilder<PublisherBook> builder)
        {
            builder.ToTable("PublishersBooks");

            builder.HasKey(pc => pc.PublisherBookId);

            builder.Property(pc => pc.PublisherId)
                .IsRequired();


            builder.Property(pc => pc.BookId)
                .IsRequired();

            builder.HasOne(pc => pc.Book)
                .WithMany(c => c.PublisherBooks)
                .HasForeignKey(pc => pc.BookId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(pc => pc.Publisher)
                .WithMany(p => p.PublisherBooks)
                .HasForeignKey(pc => pc.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
