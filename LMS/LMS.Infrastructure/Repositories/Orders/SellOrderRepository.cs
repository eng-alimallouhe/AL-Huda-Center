using LMS.Domain.Entities.Orders;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

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
            var order = await _context.Orders.FindAsync(id);
            if (order is not null)
            {
                order.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("SellOrder not found");
            }
        }
    }
}
