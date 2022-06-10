using BookCatalog.DAL.Interfaces;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Paging;
using BookCatalog.Domain.Response;
using BookCatalog.Service.Interfaces;

namespace BookCatalog.Service.Implementations
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRep;
        public BookService(IBookRepository bookRep)=> _bookRep = bookRep;
        public async Task<IBaseResponse<BookDto>> CreateServiceAsync(BookDto model)
        {
            var baseResponse = new BaseResponse<BookDto>();
            if (model.ISBN.Length == 10)
            {
                model.ISBN = new string(model.ISBN.Insert(3, "-").Insert(6, "-").Insert(11, "-"));
            }
            if (model.ISBN.Length == 13)
            {
                model.ISBN = new string(model.ISBN.Insert(3, "-").Insert(5, "-").Insert(14, "-"));
            }
            var book = await _bookRep.CreateAsync(model);
            if (book is null)
            {
                baseResponse.DisplayMessage = "Книга создана.";
            }
            else
            {
                baseResponse.DisplayMessage = "Книга не создана.";
            }
            baseResponse.Result = book;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _bookRep.DeleteAsync(id);
            if (IsSuccess)
            {
                baseResponse.DisplayMessage = "Книга удалена.";
            }
            if (!IsSuccess)
            {
                baseResponse.DisplayMessage = $"Книга c указанным id: {id} не найдена.";
            }
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<BookDto>>> GetServiceAsync(PagingQueryParameters paging, string author)
        {
            var baseResponse = new BaseResponse<PagedList<BookDto>>();
            IEnumerable<BookDto> books = await _bookRep.GetByAuthorAsync(author);
            if (books is null)
            {
                baseResponse.DisplayMessage = $"Книги под именем автора [{author}] не найдены.";
            }
            else
            {
                baseResponse.DisplayMessage = $"Вывод книг под именем автора [{author}].";
            }
            baseResponse.Result = PagedList<BookDto>.ToPagedList(books, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<BookDto>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<BookDto>();
            BookDto book = await _bookRep.GetByIdAsync(id);
            if (book is null)
            {
                baseResponse.DisplayMessage = $"Книга под id [{id}] не найдена";
            }
            else
            {
                baseResponse.DisplayMessage = $"Вывод книги под id [{id}]";
            }
            baseResponse.Result = book;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<BookDto>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<BookDto>>();
            var books = await _bookRep.GetAsync();
            if (books is null)
            {
                baseResponse.DisplayMessage = "Список всех книг пуст.";
            }
            else
            {
                baseResponse.DisplayMessage = "Список всех книг.";
            }
            baseResponse.Result = PagedList<BookDto>.ToPagedList(books, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<BookDto>> UpdateServiceAsync(BookDto model)
        {
            if (model.ISBN.Length == 10)
            {
                model.ISBN.Insert(4, "-").Insert(7, "-").Insert(model.ISBN.Length - 2, "-");
            }
            if (model.ISBN.Length == 13)
            {
                model.ISBN.Insert(4, "-").Insert(6, "").Insert(model.ISBN.Length - 2, "-");
            }
            var baseResponse = new BaseResponse<BookDto>();
            BookDto book = await _bookRep.UpdateAsync(model);
            baseResponse.DisplayMessage = "Книга обновилась.";
            baseResponse.Result = book;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<string>>> GetByGenreServiceAsync(PagingQueryParameters paging, string genre)
        {
            var baseResponse = new BaseResponse<PagedList<string>>();
            var books = await _bookRep.GetAsync();
            var result = books.Where(book => book.Genres
                .Any(g => g.GenreName.ToUpper().Replace(" ", "") == genre.ToUpper().Replace(" ", "")))
                .Select(x => x.BookName).ToList();
            if (result is null)
            {
                baseResponse.DisplayMessage = $"Книги по введенному жанру [{genre}] не найдены";
            }
            else
            {
                baseResponse.DisplayMessage = $"Книги по введенному жанру [{genre}].";
            }
            baseResponse.Result= PagedList<string>.ToPagedList(result, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
    }
}
