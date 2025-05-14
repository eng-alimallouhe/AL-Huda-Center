using LMS.Domain.Entities.Stock.Genres;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Genres
{
    public class GenreTranslationRepository : BaseRepository<GenreTranslation>
    {
        public GenreTranslationRepository(LMSDbContext context) : base(context) { }
    }
}
