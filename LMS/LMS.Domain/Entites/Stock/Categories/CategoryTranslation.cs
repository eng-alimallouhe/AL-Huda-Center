using LMS.Domain.Entites.Stock.Authors;
using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entites.Stock.Categories
{
    public class CategoryTranslation
    {
        //Primary key:
        public Guid TranslationId { get; set; }


        //Foreign key: AuthorId ==> one(author)--to--many(authorTranslation)
        public Guid CategoryId { get; set; }


        public Language Language { get; set; }
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }


        //Navigation property:
        public Category Category { get; set; }

        public CategoryTranslation()
        {
            TranslationId = Guid.NewGuid();
            Category = null!;
        }
    }
}
