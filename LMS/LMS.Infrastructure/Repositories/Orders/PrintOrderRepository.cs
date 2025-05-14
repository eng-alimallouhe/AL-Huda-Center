using LMS.Common.Exceptions;
using LMS.Domain.Entities.Orders;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Orders
{
    public class PrintOrderRepository : SoftDeletableRepository<PrintOrder>
    {
        private readonly LMSDbContext _context;
        public PrintOrderRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var printOrder = await _context.PrintOrders.FindAsync(id);
            if (printOrder is not null)
            {
                printOrder.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("PrintOrder not found");
            }
        }
    }
}
