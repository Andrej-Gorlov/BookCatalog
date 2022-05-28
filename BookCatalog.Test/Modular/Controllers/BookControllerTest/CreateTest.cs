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
    public class CreateTest
    {
        private readonly Mock<IBookService> _bookService = new Mock<IBookService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 201
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ShouldReturn201Status()
        {
            /// Arrange
            BaseResponse<BookDto> response = new();
            response.Result = BookMockData.Model();
            _bookService.Setup(_ => _.CreateServiceAsync(It.IsAny<BookDto>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (CreatedAtActionResult)await bookController.Create(BookMockData.Model());
            /// Assert
            result.StatusCode.Should().Be(201);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ShouldReturn400Status()
        {
            /// Arrange
            BaseResponse<BookDto> response = new();
            BookDto book = new() {ISBN="01234567899"};
            response.Result = book;
            _bookService.Setup(_ => _.CreateServiceAsync(It.IsAny<BookDto>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (BadRequestObjectResult)await bookController.Create(response.Result);
            /// Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
