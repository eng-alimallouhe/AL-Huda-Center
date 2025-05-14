using LMS.Domain.Entities.Stock.Products;

namespace LMS.Domain.Entities.Stock.Authors
{
    public class Author
    {
        // Primary key:
        public Guid AuthorId { get; set; }


        //soft delete
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public ICollection<Book> Books { get; set; }
        public ICollection<AuthorTranslation> Translations { get; set; }


        public Author()
        {
            AuthorId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Books = [];
            Translations = [];
        }
    }

}
