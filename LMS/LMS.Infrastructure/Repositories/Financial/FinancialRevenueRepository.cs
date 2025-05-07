using LMS.Domain.Entities.Financial;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.OrderManagement
{
    public class FinancialRevenueRepository : SoftDeletableRepository<FinancialRevenue>
    {
        private readonly LMSDbContext _context;
        public FinancialRevenueRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var revenue = await _context.FinancialRevenues.FindAsync(id);
            if (revenue is not null)
            {
                revenue.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Payment not found");
            }
        }
    }
}
