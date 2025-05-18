using LMS.Domain.Entities.Stock.Genres;
using LMS.Domain.Entities.Stock.Publishers;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Publishers
{
    public class PublisherTranslationRepository : BaseRepository<PublisherTranslation>
    {
        public PublisherTranslationRepository(LMSDbContext context) : base(context) { }
    }
}
