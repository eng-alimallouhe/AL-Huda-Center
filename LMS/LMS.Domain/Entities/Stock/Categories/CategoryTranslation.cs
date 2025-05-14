using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Stock.Categories
{
    public class CategoryTranslation
    {
        //Primary Key:
        public Guid TranslationId { get; set; }


        //Foreign Key: CategoryIdId ==> one(category)-to-many(translation) relationship

        public Guid CategoryId { get; set; }


        public Language Language { get; set; }
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }



        //Navigation Property:
        public Category Category { get; set; }


        public CategoryTranslation()
        {
            CategoryId = Guid.NewGuid();
            Category = null!;
        }
    }
}
