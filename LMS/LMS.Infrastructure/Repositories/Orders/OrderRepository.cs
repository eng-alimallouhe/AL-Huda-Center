using LMS.Infrastructure.DbContexts;
using LMS.Domain.Entities.Orders;

namespace LMS.Infrastructure.Repositories.Orders
{
    public class OrderRepository : SoftDeletableRepository<Order>
    {
        private readonly LMSDbContext _context;
        
        public OrderRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        
        public override async Task SoftDeleteAsync(Guid id)
        {
            var sellOrder = await _context.Orders.FindAsync(id);
            if (sellOrder is not null)
            {
                sellOrder.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("SellOrder not found");
            }
        }
    }
}
