using LMS.Domain.Entites.Stock.Authors;
using LMS.Domain.Entites.Stock.Genres;
using LMS.Domain.Entites.Stock.Publishers;

namespace LMS.Domain.Entites.Stock.Products
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
            Publishers = [];
            Genres = [];
            Author = null!;
        }
    }
}