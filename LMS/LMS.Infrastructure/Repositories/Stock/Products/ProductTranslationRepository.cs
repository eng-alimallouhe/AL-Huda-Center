using LMS.Domain.Entities.Stock.Genres;
using LMS.Domain.Entities.Stock.Products;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Products
{
    public class ProductTranslationRepository : BaseRepository<ProductTranslation>
    {
        public ProductTranslationRepository(LMSDbContext context) : base(context) { }
    }
}
