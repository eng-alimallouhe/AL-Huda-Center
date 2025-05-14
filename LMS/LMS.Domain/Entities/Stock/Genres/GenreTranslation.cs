using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Stock.Genres
{
    public class GenreTranslation
    {
        //Primary Key:
        public Guid TranslationId { get; set; }


        //Foreign Key: GenreId ==> one(genre)-to-many(translation) relationship
        public Guid GenreId { get; set; }


        public Language Language { get; set; }
        public required string GenreName { get; set; }
        public required string GenreDescription { get; set; }


        // Navigation property:
        public Genre Genre { get; set; }

        public GenreTranslation()
        {
            TranslationId = Guid.NewGuid();
            Genre = null!;
        }
    }
}
