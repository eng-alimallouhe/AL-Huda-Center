using LMS.Domain.Entities.Stock.Genres;
using LMS.Domain.Entities.Stock.Products;

namespace LMS.Domain.Entities.Stock.PublicEntities
{
    public class GenreBook
    {
        public Guid GenreBookId { get; set; }
        public Guid GenreId { get; set; }
        public Guid BookId { get; set; }


        public Book Book { get; set; }
        public Genre Genre { get; set; }

        public GenreBook()
        {
            GenreBookId = Guid.NewGuid();
            Book = null!;
            Genre = null!;
        }
    }
}
