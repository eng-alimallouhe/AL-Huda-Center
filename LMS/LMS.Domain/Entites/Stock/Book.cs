namespace LMS.Domain.Entites.Stock
{
    public class Book : Product
    {
        //Foreign Key: CategoryId ==> one(Genre)-to-many(Book) relationship
        public Guid GenreId { get; set; }


        //Foreign Key: AuthorId ==> one(Author)-to-many(Author) relationship
        public Guid AuthorId { get; set; }


        public required string ISBN { get; set; }
        public int Pages { get; set; }
        public decimal RentalCost { get; set; }
        public int PublishedYear { get; set; }


        //Navigation Property:
        public ICollection<Publisher> Publishers { get; set; }
        public ICollection<Genre> Genre { get; set; }
        public Author Author { get; set; }


        public Book()
        {
            Publishers = [];
            Genre = [];
            Author = null!;
        }
    }
}