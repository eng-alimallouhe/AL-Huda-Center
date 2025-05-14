using LMS.Domain.Entities.Stock.Genres;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Products
{
    public class ProductTranslationRepository : BaseRepository<GenreTranslation>
    {
        public ProductTranslationRepository(LMSDbContext context) : base(context) { }
    }
}
