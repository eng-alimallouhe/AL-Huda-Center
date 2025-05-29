using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Orders
{
    public class BaseOrderRepository : SoftDeletableRepository<BaseOrder>
    {
        private readonly LMSDbContext _context;
        public BaseOrderRepository(LMSDbContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task SoftDeleteAsync(Guid id)
        {
            var baseOrder = await _context.BaseOrders.FindAsync(id);

            if (baseOrder is null)
                throw new EntryPointNotFoundException();

            _context.BaseOrders.Remove(baseOrder);
        }
    }
}
