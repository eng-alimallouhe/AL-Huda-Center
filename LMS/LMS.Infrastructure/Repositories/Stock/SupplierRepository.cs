using LMS.Domain.Entities.Stock;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.Stock
{
    public class SupplierRepository : SoftDeletableRepository<Supplier>
    {
        private readonly LMSDbContext _context;
        public SupplierRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier is not null)
            {
                supplier.IsActive = false;
                supplier.UpdatedAt = DateTime.UtcNow;
                _context.Suppliers.Update(supplier);
                await SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Supplier not found");
            }
        }
    }
}
