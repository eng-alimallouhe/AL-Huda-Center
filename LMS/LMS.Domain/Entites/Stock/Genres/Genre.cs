using LMS.Domain.Entites.Stock.Products;

namespace LMS.Domain.Entites.Stock.Genres
{
    public class Genre
    {
        // Primary key:
        public Guid GenreId { get; set; }


        //soft delete
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        // Navigation property:
        public ICollection<Book> Books { get; set; }
        public ICollection<GenreTranslation> Translations { get; set; }


        public Genre()
        {
            GenreId = Guid.NewGuid();
            IsActive = true;
            Books = [];
            Translations = [];
        }
    }
}
