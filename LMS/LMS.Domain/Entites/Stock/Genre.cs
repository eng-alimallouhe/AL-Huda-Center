namespace LMS.Domain.Entites.Stock
{
    public class Genre
    {
        // Primary key:
        public Guid GenreId { get; set; }


        public required string GenreName { get; set; }
        public required string GenreDescription { get; set; }


        //soft delete
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        // Navigation property:
        public ICollection<Book> Books { get; set; }

        public Genre()
        {
            GenreId = Guid.NewGuid();
            IsActive = true;
            Books = [];
        }
    }
}
