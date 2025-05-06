using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Stock.Publishers
{
    public class PublisherTranslation
    {
        // Primary key:
        public Guid TranslationId { get; set; }


        //Foreign key: PubkisgerId ==> one(publisher)--to--many(publisherTranslation)
        public Guid PublisherId { get; set; }


        public Language Language { get; set; }
        public required string PublisherName { get; set; }
        public required string PublisherDescription { get; set; }


        public Publisher Publisher { get; set; }


        public PublisherTranslation()
        {
            TranslationId = Guid.NewGuid();
            Publisher = null!;
        }
    }
}
