using BookCatalog.Controllers;
using BookCatalog.Domain.Entity.Dto;
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
    public class GetByIdTest
    {
        private readonly Mock<IBookService> _bookService = new Mock<IBookService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_ShouldReturn200Status()
        {
            /// Arrange
            BaseResponse<BookDto> response = new();
            response.Result = BookMockData.GetById(BookMockData.Get().FirstOrDefault().BookId);

            _bookService.Setup(_ => _.GetByIdServiceAsync(It.IsAny<int>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (OkObjectResult)await bookController.GetById(BookMockData.Get().FirstOrDefault().BookId);
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            BaseResponse<BookDto> response = new();
            response.Result = BookMockData.GetById(id);
            _bookService.Setup(_ => _.GetByIdServiceAsync(It.IsAny<int>())).ReturnsAsync(response);
            BookController gameController = new BookController(_bookService.Object);
            /// Act
            var result = (BadRequestObjectResult)await gameController.GetById(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_ShouldReturn404Status()
        {
            /// Arrange
            BaseResponse<BookDto> response = new();
            response.Result = BookMockData.GetById(BookMockData.Get().Count()+1);
            _bookService.Setup(_ => _.GetByIdServiceAsync(It.IsAny<int>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (NotFoundObjectResult)await bookController.GetById(BookMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
