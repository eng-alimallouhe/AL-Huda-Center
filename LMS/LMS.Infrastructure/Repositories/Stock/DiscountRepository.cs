using LMS.Common.Exceptions;
using LMS.Domain.Entities.Stock;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock
{
    public class DiscountRepository : SoftDeletableRepository<Discount>
    {
        private readonly LMSDbContext _context;

        public DiscountRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task SoftDeleteAsync(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author is not null)
            {
                author.IsActive = false;
                author.UpdatedAt = DateTime.Now;
                _context.Authors.Update(author);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Author not found");
            }
        }
    }
}
