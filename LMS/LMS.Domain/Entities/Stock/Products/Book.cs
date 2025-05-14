using LMS.Domain.Entities.Stock.Authors;
using LMS.Domain.Entities.Stock.Genres;
using LMS.Domain.Entities.Stock.Publishers;

namespace LMS.Domain.Entities.Stock.Products
{
    public class Book : Product
    {
        //Foreign Key: AuthorId ==> one(Author)-to-many(Author) relationship
        public Guid AuthorId { get; set; }
        

        public required string ISBN { get; set; }
        public int Pages { get; set; }
        public decimal RentalCost { get; set; }
        public int PublishedYear { get; set; }


        //Navigation Property:
        public ICollection<Publisher> Publishers { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public Author Author { get; set; }


        public Book()
        {
            Author = null!;
            Publishers = [];
            Genres = [];
        }
    }
}