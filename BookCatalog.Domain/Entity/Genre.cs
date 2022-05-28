using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain.Entity
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public int BookId { get; set; }
        public string? GenreName { get; set; }
    }
}
