using LMS.Domain.Entities.Stock.Genres;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock
{
    public class GenreTranslationRepository : BaseRepository<Genre>
    {
        public GenreTranslationRepository(LMSDbContext context) : base(context)
        {
        }
    }
}
