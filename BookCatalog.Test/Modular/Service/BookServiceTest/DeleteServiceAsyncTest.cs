using BookCatalog.DAL.Interfaces;
using BookCatalog.Domain.Response;
using BookCatalog.Service.Implementations;
using BookCatalog.Test.MockData;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Service.BookServiceTest
{
    public class DeleteServiceAsyncTest
    {
        private readonly Mock<IBookRepository> _bookRepository = new Mock<IBookRepository>();
        /// <summary>
        /// Проверяет что обработчик возвращает true 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteServiceAsync_ReturnsTrue()
        {
            /// Arrange
            _bookRepository.Setup(_ => _.DeleteAsync(It.IsAny<int>())).ReturnsAsync(BookMockData.Delete(BookMockData.Get().FirstOrDefault().BookId));
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = (BaseResponse<bool>)await bookService.DeleteServiceAsync(BookMockData.Get().FirstOrDefault().BookId);
            /// Assert
            Assert.True(result.Result);
            Assert.Equal(result.DisplayMessage, "Книга удалена.");
        }
        /// <summary>
        /// Проверяет что обработчик возвращает false 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteServiceAsync_ReturnsFalse()
        {
            /// Arrange
            _bookRepository.Setup(_ => _.DeleteAsync(It.IsAny<int>())).ReturnsAsync(BookMockData.Delete(BookMockData.Get().Count()+1));
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = (BaseResponse<bool>)await bookService.DeleteServiceAsync(BookMockData.Get().Count() + 1);
            /// Assert
            Assert.False(result.Result);
            Assert.Equal(result.DisplayMessage, $"Книга c указанным id: {BookMockData.Get().Count() + 1} не найдена.");
        }
    }
}
