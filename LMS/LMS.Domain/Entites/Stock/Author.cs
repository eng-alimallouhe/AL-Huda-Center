namespace LMS.Domain.Entites.Stock
{
    public class Author
    {
        // Primary key:
        public Guid AuthorId { get; set; }


        public string AuthorName { get; set; } = string.Empty;
        public string AuthorDescription { get; set; } = string.Empty;


        //soft delete
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public ICollection<Book> Books { get; set; }


        public Author()
        {
            AuthorId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Books = [];
        }
    }
}
