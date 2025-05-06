using LMS.Domain.Entities.Stock.Products;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.Stock.Products
{
    public class ProductRepository : SoftDeletableRepository<Product>
    {
        private readonly LMSDbContext _context;
        public ProductRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is not null)
            {
                product.IsActive = false;
                product.UpdatedAt = DateTime.UtcNow;
                _context.Products.Update(product);
                await SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Product not found");
            }
        }
    }
}
