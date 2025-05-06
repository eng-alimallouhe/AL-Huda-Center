using LMS.Domain.Entities.Stock.Products;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Products
{
    public class ProductTranslationRepository : BaseRepository<ProductTranslation>
    {
        private readonly LMSDbContext _context;
        public ProductTranslationRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
