using LMS.Domain.Entities.Stock.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infrastructure.Configurations.Stock.Authors
{
    public class AuthorConfigurations :
        IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(a => a.AuthorId);
            
            builder.Property(a => a.IsActive)
                    .IsRequired();
            
            builder.Property(a => a.CreatedAt)
                    .IsRequired();
            
            builder.Property(a => a.UpdatedAt)
                    .IsRequired();
        }
    }
}
