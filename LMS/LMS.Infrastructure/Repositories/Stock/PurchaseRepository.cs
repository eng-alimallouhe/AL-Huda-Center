using LMS.Common.Exceptions;
using LMS.Domain.Entities.Stock;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock
{
    public class PurchaseRepository : SoftDeletableRepository<Purchase>
    {
        private readonly LMSDbContext _context;
        public PurchaseRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase is not null)
            {
                purchase.IsActive = false;
                purchase.UpdatedAt = DateTime.UtcNow;
                _context.Purchases.Update(purchase);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Purchase not found");
            }
        }
    }
}
