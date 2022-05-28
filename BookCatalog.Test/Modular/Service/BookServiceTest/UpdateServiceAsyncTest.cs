using BookCatalog.DAL.Interfaces;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Response;
using BookCatalog.Service.Implementations;
using BookCatalog.Test.MockData;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Service.BookServiceTest
{
    public class UpdateServiceAsyncTest
    {
        private readonly Mock<IBookRepository> _bookRepository = new Mock<IBookRepository>();

        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateServiceAsync_ReturnsRightType()
        {
            /// Arrange
            _bookRepository.Setup(_ => _.UpdateAsync(It.IsAny<BookDto>())).ReturnsAsync(BookMockData.Model());
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = await bookService.UpdateServiceAsync(BookMockData.Model());
            /// Assert
            Assert.IsType<BaseResponse<BookDto>>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateServiceAsync_ReturnsRight()
        {
            /// Arrange
            _bookRepository.Setup(_ => _.UpdateAsync(It.IsAny<BookDto>())).ReturnsAsync(BookMockData.Model());
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = (BaseResponse<BookDto>)await bookService.UpdateServiceAsync(BookMockData.Model());
            /// Assert
            Assert.IsType<string>(result.DisplayMessage);
            Assert.Equal(result.DisplayMessage, "Книга обновилась.");
        }
    }
}
