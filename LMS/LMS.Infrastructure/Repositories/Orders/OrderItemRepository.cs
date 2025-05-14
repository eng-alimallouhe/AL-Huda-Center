using LMS.Domain.Entities.Orders;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Orders
{
    public class OrderItemRepository : SoftDeletableRepository<OrderItem>
    {
        private readonly LMSDbContext _context;
        public OrderItemRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem is not null)
            {
                orderItem.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("OrderItem not found");
            }
        }
    }
}
