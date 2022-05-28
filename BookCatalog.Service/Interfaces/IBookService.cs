using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Paging;
using BookCatalog.Domain.Response;

namespace BookCatalog.Service.Interfaces
{
    public interface IBookService : IBaseService <BookDto>
    {
        Task<IBaseResponse<BookDto>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<BookDto>>> GetServiceAsync(PagingQueryParameters paging, string author);
        Task<IBaseResponse<PagedList<string>>> GetByGenreServiceAsync(PagingQueryParameters paging, string genre);
    }
}
