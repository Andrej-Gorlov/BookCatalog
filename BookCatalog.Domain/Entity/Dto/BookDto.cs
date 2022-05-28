using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain.Entity.Dto
{
    public class BookDto
    {
        public int BookId { get; set; }
        [Required(ErrorMessage = "Укажите название книги")]
        public string? BookName { get; set; }
        [Required(ErrorMessage = "Укажите имя автора")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        public string? Author { get; set; }
        [Required(ErrorMessage = "Укажите название жанра(ов).")]
        public List<GenreDto>? Genres { get; set; }
        [Required(ErrorMessage = "Укажите год выпуска книги")]
        public int YearOfIssue { get; set; }
        [Required(ErrorMessage = "Укажите ISBN книги")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "ISBN должен состоять из чисел")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "номер ISBN должен состоять из 10 или 13 чисел")]
        public string? ISBN { get; set; }
        public string? ImageUrl { get; set; }
        public string? ShortDescription { get; set; }
    }
}
