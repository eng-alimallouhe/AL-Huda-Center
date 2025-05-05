using LMS.Domain.Entites.Stock.Products;

namespace LMS.Domain.Entites.Stock.Publishers
{
    public class Publisher
    {
        // Primary key:
        public Guid PublisherId { get; set; }


        //soft delete
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public ICollection<Book> Books { get; set; }
        public ICollection<PublisherTranslation> Translations { get; set; }


        public Publisher()
        {
            PublisherId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Books = [];
            Translations = [];
        }
    }
}
