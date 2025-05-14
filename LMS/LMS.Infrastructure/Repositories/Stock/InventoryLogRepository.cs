using LMS.Domain.Entities.Stock;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock
{
    public class InventoryLogRepository : BaseRepository<InventoryLog>
    {
        public InventoryLogRepository(LMSDbContext context) : base(context) { }
    }
}
