using LMS.Domain.Entities.Stock.Publishers;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.Stock
{
    public class PublisherTranslationRepository : BaseRepository<PublisherTranslation>
    {
        public PublisherTranslationRepository(LMSDbContext context) : base(context)
        {
        }
    }
}
