using LMS.Domain.Entities.Stock.Authors;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Stock.Authors
{
    public class AuthorTranslationRepository : BaseRepository<AuthorTranslation>
    {
        public AuthorTranslationRepository(LMSDbContext context) : base(context) {  }
    }
}