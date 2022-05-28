using BookCatalog.Controllers;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Response;
using BookCatalog.Service.Interfaces;
using BookCatalog.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Controllers.BookControllerTest
{
    public class UpdateTest
    {
        private readonly Mock<IBookService> _bookService = new Mock<IBookService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_ShouldReturn200Status()
        {
            /// Arrange
            BaseResponse<BookDto> response = new();
            response.Result = BookMockData.Model();
            _bookService.Setup(_ => _.UpdateServiceAsync(It.IsAny<BookDto>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (OkObjectResult)await bookController.Update(BookMockData.Model());
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_ShouldReturn404Status()
        {
            /// Arrange
            BaseResponse<BookDto> response = new();
            _bookService.Setup(_ => _.UpdateServiceAsync(It.IsAny<BookDto>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (NotFoundObjectResult)await bookController.Update(BookMockData.Model());
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
