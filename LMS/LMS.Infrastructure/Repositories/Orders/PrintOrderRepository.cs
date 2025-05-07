using LMS.Domain.Entities.Orders;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.Orders
{
    public class PrintingOrderRepository : SoftDeletableRepository<PrintingOrder>
    {
        private readonly LMSDbContext _context;
        public PrintingOrderRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var printOrder = await _context.PrintingOrders.FindAsync(id);
            if (printOrder is not null)
            {
                printOrder.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("PrintOrder not found");
            }
        }
    }
}
