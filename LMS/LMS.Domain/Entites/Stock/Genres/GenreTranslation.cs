using LMS.Domain.Entities.Stock.Authors;
using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Stock.Genres
{
    public class GenreTranslation
    {
        //Primary key:
        public Guid TranslationId { get; set; }


        //Foreign key: GenreId ==> one(genre)--to--many(genreTranslation)
        public Guid GenreId { get; set; }


        public Language Language { get; set; }
        public required string GenreName { get; set; }
        public required string GenreDescription { get; set; }


        //Navigation property:
        public Genre Genre { get; set; }


        public GenreTranslation()
        {
            TranslationId = Guid.NewGuid();
            Genre = null!;
        }
    }
}
