using BookCatalog.Domain.Entity.Dto;

namespace BookCatalog.DAL.Interfaces
{
    public interface IBookRepository: IBaseRepository<BookDto>
    {
        Task<IEnumerable<BookDto>> GetByAuthorAsync(string author);
        Task<BookDto> GetByIdAsync(int id);
    }
}
