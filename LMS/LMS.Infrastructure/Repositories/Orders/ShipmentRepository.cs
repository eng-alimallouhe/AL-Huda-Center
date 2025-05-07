using LMS.Domain.Entities.Orders;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.Orders
{
    public class ShipmentRepository : SoftDeletableRepository<Shipment>
    {
        private readonly LMSDbContext _context;
        public ShipmentRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var deliveryOrder = await _context.Shipments.FindAsync(id);
            if (deliveryOrder is not null)
            {
                deliveryOrder.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("DeliveryOrder not found");
            }
        }
    }
}
