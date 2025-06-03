using LMS.Domain.Entities.Stock.Genres;
using LMS.Domain.Entities.Stock.Products;
using LMS.Domain.Entities.Stock.Publishers;

namespace LMS.Domain.Entities.Stock.PublicEntities
{
    public class PublisherBook
    {
        public Guid PublisherBookId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid BookId { get; set; }


        public Book Book { get; set; }
        public Publisher Publisher { get; set; }

        public PublisherBook()
        {
            PublisherBookId = Guid.NewGuid();
            Book = null!;
            Publisher = null!;
        }
    }
}
