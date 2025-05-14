using LMS.Common.Exceptions;
using LMS.Domain.Entities.Stock.Products;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Products
{
    public class BookRepository : SoftDeletableRepository<Book>
    {
        private readonly LMSDbContext _context;

        public BookRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task SoftDeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is not null)
            {
                book.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Book not found");
            }
        }
    }
}
