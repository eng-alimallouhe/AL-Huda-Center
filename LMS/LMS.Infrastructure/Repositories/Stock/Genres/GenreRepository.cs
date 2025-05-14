using LMS.Common.Exceptions;
using LMS.Domain.Entities.Stock.Genres;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Genres
{
    public class GenreRepository : SoftDeletableRepository<Genre>
    {
        private readonly LMSDbContext _context;
        public GenreRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre is not null)
            {
                genre.IsActive = false;
                genre.UpdatedAt = DateTime.UtcNow;
                _context.Genres.Update(genre);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Genre not found");
            }
        }
    }
}
