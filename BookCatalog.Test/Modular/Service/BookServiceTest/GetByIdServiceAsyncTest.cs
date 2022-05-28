using BookCatalog.DAL.Interfaces;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Response;
using BookCatalog.Service.Implementations;
using BookCatalog.Test.MockData;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Service.BookServiceTest
{
    public class GetByIdServiceAsyncTest
    {
        private readonly Mock<IBookRepository> _bookRepository = new Mock<IBookRepository>();

        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdServiceAsync_ReturnsRightType()
        {
            /// Arrange
            _bookRepository.Setup(_ => _.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(BookMockData.GetById(BookMockData.Get().FirstOrDefault().BookId));
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = await bookService.GetByIdServiceAsync(BookMockData.Get().FirstOrDefault().BookId);
            /// Assert
            Assert.IsType<BaseResponse<BookDto>>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdServiceAsync_ReturnsRight()
        {
            /// Arrange
            _bookRepository.Setup(_ => _.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(BookMockData.GetById(BookMockData.Get().FirstOrDefault().BookId));
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = (BaseResponse<BookDto>)await bookService.GetByIdServiceAsync(BookMockData.Get().FirstOrDefault().BookId);
            /// Assert
            Assert.NotNull(result.Result);
            Assert.Equal(result.DisplayMessage, $"Вывод книги под id [{BookMockData.Get().FirstOrDefault().BookId}]");
        }
        /// <summary>
        /// Проверяет что обработчик возвращает Null 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdServiceAsync_ReturnsNuul()
        {
            /// Arrange
            BookDto? book = null;
            _bookRepository.Setup(_ => _.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(BookMockData.GetById(BookMockData.Get().LastOrDefault().BookId+1));
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = (BaseResponse<BookDto>)await bookService.GetByIdServiceAsync(BookMockData.Get().Count()+1);
            /// Assert
            Assert.Null(result.Result);
            Assert.Equal(result.DisplayMessage, $"Книга под id [{BookMockData.Get().Count() + 1}] не найдена");
        }
    }
}
