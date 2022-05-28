using BookCatalog.Domain.Paging;
using BookCatalog.Domain.Response;

namespace BookCatalog.Service.Interfaces
{
    public interface IBaseService<T>
    {
        Task<IBaseResponse<PagedList<T>>> GetServiceAsync(PagingQueryParameters paging);
        Task<IBaseResponse<T>> CreateServiceAsync(T entity);
        Task<IBaseResponse<T>> UpdateServiceAsync(T entity);
        Task<IBaseResponse<bool>> DeleteServiceAsync(int id);
    }
}
