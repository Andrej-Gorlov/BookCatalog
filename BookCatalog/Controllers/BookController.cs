using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Paging;
using BookCatalog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookCatalog.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookSer;
        public BookController(IBookService bookSer) => _bookSer = bookSer;

        /// <summary>
        /// Список всех книг.
        /// </summary>
        /// <param name="paging"></param>
        /// <returns>Вывод всех книг</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /books
        ///     
        ///        PageNumber: Номер страницы   // Введите номер страницы, которую нужно показать с списоком всех книг.
        ///        PageSize: Размер страницы    // Введите размер страницы, какое количество книг нужно вывести.
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список книг не найден. </response>
        [HttpGet]
        [Route("books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var books = await _bookSer.GetServiceAsync(paging);
            if (books.Result is null)
            {
                return NotFound(books);
            }
            var metadata = new
            {
                books.Result.TotalCount,
                books.Result.PageSize,
                books.Result.CurrentPage,
                books.Result.TotalPages,
                books.Result.HasNext,
                books.Result.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(books);
        }
        /// <summary>
        /// список всех книг по полному имени автора.
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="author"></param>
        /// <returns>Вывод всех книг</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /book/{author}
        ///     
        ///        PageNumber: Номер страницы   // Введите номер страницы, которую нужно показать с списоком всех книг.
        ///        PageSize: Размер страницы    // Введите размер страницы, какое количество книг нужно вывести.
        ///        Author: "string"             // Введите полное имя автора, список книг которого нужно показать.
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список книг не найден. </response>
        [HttpGet]
        [Route("books/{author}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging,string author)
        {
            var books = await _bookSer.GetServiceAsync(paging,author);
            if (books.Result is null)
            {
                return NotFound(books);
            }
            var metadata = new
            {
                books.Result.TotalCount,
                books.Result.PageSize,
                books.Result.CurrentPage,
                books.Result.TotalPages,
                books.Result.HasNext,
                books.Result.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(books);
        }
        /// <summary>
        /// Вывод книги по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вывод данных книги</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /book/{id:int}
        ///     
        ///        Id: 0   // Введите id книги, которую нужно показать.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Книга не найдена </response>
        [HttpGet]
        [Route("book/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            var book = await _bookSer.GetByIdServiceAsync(id);
            if (book.Result is null)
            {
                return NotFound(book);
            }
            return Ok(book);
        }
        /// <summary>
        /// Список названий книг по жанру.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>Вывод списка названий книг по жанру.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /book/{genre}
        ///     
        ///        PageNumber: Номер страницы   // Введите номер страницы, которую нужно показать с списоком всех названий книг.
        ///        PageSize: Размер страницы    // Введите размер страницы, какое количество названий книг нужно вывести.
        ///        genre: название жанра        // Введите название жанра, для вывода списка названий книг которому они соответствуют.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список названий книг не найден. </response>
        [HttpGet]
        [Route("bookNames/{genre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByGenre([FromQuery] PagingQueryParameters paging, string genre)
        {
            var bookNames = await _bookSer.GetByGenreServiceAsync(paging, genre);
            if (bookNames.Result is null)
            {
                return NotFound(bookNames);
            }
            var metadata = new
            {
                bookNames.Result.TotalCount,
                bookNames.Result.PageSize,
                bookNames.Result.CurrentPage,
                bookNames.Result.TotalPages,
                bookNames.Result.HasNext,
                bookNames.Result.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(bookNames);
        }
        /// <summary>
        /// Создание новой книги.
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns>Создаётся книга</returns>
        /// <remarks>
        /// 
        ///     Свойство ["BookId", "ImageUrl" и "ShortDescription"] указывать не обязательно.
        ///     Свойство ["ISBN"] введите без пробелов и дефисов (состоит из 10 или 13 значений)
        /// 
        /// Образец ввовда данных:
        ///
        ///     POST /book
        ///     
        ///     {
        ///       "BookId": 0,                   // id книги.
        ///       "BookName": "string",          // Название книги.                            
        ///       "Author": "string",            // Полное имя автора.
        ///       "YearOfIssue": 0               // Год выпуска.
        ///       "ISBN": "string",              // Номер книги. 
        ///       "Genres": [                    // Данные жанра.
        ///         {
        ///           "GenreId": 0,              // id жанра.
        ///           "BookId": 0,               // id книги, которой принадлежит жанр.
        ///           "GenreName": "string"      // Название жанра.
        ///         }         
        ///       ],
        ///       "ImageUrl": "string",          // Url на изображение обложки книг.
        ///       "ShortDescription": "string"   // Краткое описание книги.
        ///     }
        ///
        /// </remarks>
        /// <response code="201"> Книга создан. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [Route("book")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] BookDto bookDto) 
        {
            if (bookDto.ISBN.Length==11 || bookDto.ISBN.Length==12)
            {
                return BadRequest($"Номер книги должен быть 10 или 13 значный");
            }
            var book = await _bookSer.CreateServiceAsync(bookDto);
            if (book.Result is null)
            {
                return BadRequest(book);
            }
            return CreatedAtAction(nameof(Get), book);
        }
        /// <summary>
        /// Редактирование книги.
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns>Обновление книги.</returns>
        /// <remarks>
        /// 
        /// Образец ввовда данных:
        ///
        ///     PUT /book
        ///     
        ///     {
        ///       "BookId": 0,                   // id книги.
        ///       "BookName": "string",          // Название книги.                            
        ///       "Author": "string",            // Полное имя автора.
        ///       "YearOfIssue": 0               // Год выпуска.
        ///       "ISBN": "string",              // Номер книги.     
        ///       "Genres": [                    // Данные жанра.
        ///         {
        ///           "GenreId": 0,              // id жанра.
        ///           "BookId": 0,               // id книги, которой принадлежит жанр.
        ///           "GenreName": "string"      // Название жанра.
        ///         }         
        ///       ],
        ///       "ImageUrl": "string",          // Url на изображение обложки книг.
        ///       "ShortDescription": "string"   // Краткое описание книги.
        ///     }
        ///
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        /// <response code="404"> Книга не найдена. </response>
        [HttpPut]
        [Route("book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] BookDto bookDto)
        {
            if (bookDto.ISBN.Length == 11 || bookDto.ISBN.Length == 12)
            {
                return BadRequest($"Номер книги должен быть 10 или 13 значный");
            }
            var book = await _bookSer.UpdateServiceAsync(bookDto);
            if (book.Result is null)
            {
                return NotFound(book);
            }
            return Ok(book);
        }
        /// <summary>
        /// Удаление книги.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Книга удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /book/{id}
        ///     
        ///        Id: 0   // Введите id книги, которую нужно удалить.
        ///     
        /// </remarks>
        /// <response code="204"> Книга удалёна. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Книга c указанным id не найден. </response>
        [HttpDelete]
        [Route("book/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            var user = await _bookSer.DeleteServiceAsync(id);
            if (user.Result is false)
            {
                return NotFound(user);
            }
            return NoContent();
        }
    }
}
