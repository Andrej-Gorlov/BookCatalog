using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain.Entity
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string? BookName { get; set; }
        public string? Author { get; set; }
        public List<Genre>? Genres { get; set; }
        public int YearOfIssue { get; set; }
        public string? ISBN { get; set; }
        public string? ImageUrl { get; set; }
        public string? ShortDescription { get; set; }
    }
}
