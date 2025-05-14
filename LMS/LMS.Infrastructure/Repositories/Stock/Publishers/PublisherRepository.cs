using LMS.Common.Exceptions;
using LMS.Domain.Entities.Stock.Publishers;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Publishers
{
    public class PublisherRepository : SoftDeletableRepository<Publisher>
    {
        private readonly LMSDbContext _context;
        public PublisherRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher is not null)
            {
                publisher.IsActive = false;
                publisher.UpdatedAt = DateTime.UtcNow;
                _context.Publishers.Update(publisher);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Publisher not found");
            }
        }
    }
}
