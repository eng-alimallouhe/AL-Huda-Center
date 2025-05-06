using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Stock.Authors
{
    public class AuthorTranslation
    {
        //Primary key:
        public Guid TranslationId { get; set; }


        //Foreign key: AuthorId ==> one(author)--to--many(authorTranslation)
        public Guid AuthorId { get; set; }


        public Language Language { get; set; }
        public required string AuthorName { get; set; }
        public required string AuthorDescription { get; set; }


        //Navigation property:
        public Author Author { get; set; }

        public AuthorTranslation()
        {
            TranslationId = Guid.NewGuid();
            Author = null!;
        }
    }
}
