using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain.Entity.Dto
{
    public class GenreDto
    {
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Укажите id жанра.")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Укажите название жанра.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public string? GenreName { get; set; }
    }
}
