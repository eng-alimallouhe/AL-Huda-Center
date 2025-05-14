using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Stock.Authors
{
    public class AuthorTranslation
    {
        //Primary Key:
        public Guid TranslationId { get; set; }


        //Foreign Key: AuthorId ==> one(author)-to-many(translation) relationship
        public Guid AuthorId { get; set; }

        public Language Language { get; set; }
        public required string AuthorName { get; set; }
        public required string AuthorDescription { get; set; }


        //Navigation Property:
        public Author Author { get; set; }

        public AuthorTranslation()
        {
            TranslationId = Guid.NewGuid();
            Author = null!;
        }
    }

}
