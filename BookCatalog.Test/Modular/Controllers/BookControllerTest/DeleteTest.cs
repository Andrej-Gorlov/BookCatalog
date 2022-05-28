using BookCatalog.Controllers;
using BookCatalog.Domain.Response;
using BookCatalog.Service.Interfaces;
using BookCatalog.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Controllers.BookControllerTest
{
    public class DeleteTest
    {
        private readonly Mock<IBookService> _bookService = new Mock<IBookService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 204
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_ShouldReturn204Status()
        {
            /// Arrange
            BaseResponse<bool> response = new();
            bool IsSuccess = BookMockData.Delete(BookMockData.Get().FirstOrDefault().BookId);
            response.Result = IsSuccess;
            _bookService.Setup(_ => _.DeleteServiceAsync(It.IsAny<int>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (NoContentResult)await bookController.Delete(BookMockData.Get().FirstOrDefault().BookId);
            /// Assert
            result.StatusCode.Should().Be(204);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            BaseResponse<bool> response = new();
            bool IsSuccess = BookMockData.Delete(id);
            response.Result = IsSuccess;
            _bookService.Setup(_ => _.DeleteServiceAsync(It.IsAny<int>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (BadRequestObjectResult)await bookController.Delete(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_ShouldReturn404Status()
        {
            /// Arrange
            BaseResponse<bool> response = new();
            bool IsSuccess = BookMockData.Delete(BookMockData.Get().Count() + 1);
            response.Result = IsSuccess;
            _bookService.Setup(_ => _.DeleteServiceAsync(It.IsAny<int>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (NotFoundObjectResult)await bookController.Delete(BookMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
