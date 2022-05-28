using BookCatalog.Controllers;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Paging;
using BookCatalog.Domain.Response;
using BookCatalog.Service.Interfaces;
using BookCatalog.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Controllers.BookControllerTest
{
    public class GetTest
    {
        private readonly Mock<IBookService> _bookService = new Mock<IBookService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_TwoArguments_ShouldReturn200Status()
        {
            /// Arrange
            PagingQueryParameters parameters = new();
            BaseResponse<PagedList<BookDto>> response = new();
            response.Result = BookMockData.BookByAuthor(BookMockData.Get().FirstOrDefault().Author);

            _bookService.Setup(_ => _.GetServiceAsync(parameters, It.IsAny<string>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (OkObjectResult)await bookController.Get(parameters, BookMockData.Get().FirstOrDefault().Author);
            /// Assert
            result.StatusCode.Should().Be(200);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_TwoArguments_ShouldReturn404Status()
        {
            /// Arrange
            PagingQueryParameters parameters = new();
            BaseResponse<PagedList<BookDto>> response = new();
            
            _bookService.Setup(_ => _.GetServiceAsync(parameters, It.IsAny<string>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (NotFoundObjectResult)await bookController
                .Get(parameters, new string(BookMockData.Get().FirstOrDefault().Author.Reverse().ToArray()));
            /// Assert
            result.StatusCode.Should().Be(404);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            /// Arrange
            PagingQueryParameters parameters = new();
            BaseResponse<PagedList<BookDto>> response = new();
            response.Result = PagedList<BookDto>.ToPagedList(BookMockData.Get(), BookMockData.pageNumber, BookMockData.pageSize);

            _bookService.Setup(_ => _.GetServiceAsync(parameters)).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (OkObjectResult)await bookController.Get(parameters);
            /// Assert
            result.StatusCode.Should().Be(200);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ShouldReturn404Status()
        {
            /// Arrange
            PagingQueryParameters parameters = new();
            BaseResponse<PagedList<BookDto>> response = new();

            _bookService.Setup(_ => _.GetServiceAsync(parameters)).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (NotFoundObjectResult)await bookController
                .Get(parameters);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
