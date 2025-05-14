using LMS.Domain.Entities.Stock.Genres;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Publishers
{
    public class PublisherTranslationRepository : BaseRepository<GenreTranslation>
    {
        public PublisherTranslationRepository(LMSDbContext context) : base(context) { }
    }
}
