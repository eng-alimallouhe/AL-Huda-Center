using LMS.Domain.Entities.Stock.Products;

namespace LMS.Domain.Entities.Stock.Genres
{
    public class Genre
    {
        // Primary key:
        public Guid GenreId { get; set; }


        //soft delete
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public ICollection<Book> Books { get; set; }
        public ICollection<GenreTranslation> Translations { get; set; }


        public Genre()
        {
            GenreId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Books = [];
            Translations = [];
        }
    }
}
