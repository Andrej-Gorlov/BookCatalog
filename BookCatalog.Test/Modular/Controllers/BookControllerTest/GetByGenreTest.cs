using BookCatalog.Controllers;
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
    public class GetByGenreTest
    {
        private readonly Mock<IBookService> _bookService = new Mock<IBookService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByGenre_ShouldReturn200Status()
        {
            /// Arrange
            PagingQueryParameters parameters = new();
            BaseResponse<PagedList<string>> response = new();
            var genre = BookMockData.Get().Select(x => new { 
                genre = x.Genres.FirstOrDefault().GenreName
            }).FirstOrDefault().genre;
            response.Result = BookMockData.BookByGenre(genre);

            _bookService.Setup(_ => _.GetByGenreServiceAsync(parameters, It.IsAny<string>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (OkObjectResult)await bookController.GetByGenre(parameters, genre);
            /// Assert
            result.StatusCode.Should().Be(200);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByGenre_ShouldReturn404Status()
        {
            /// Arrange
            PagingQueryParameters parameters = new();
            BaseResponse<PagedList<string>> response = new();
            var genre = BookMockData.Get().Select(x => new {
                genre = x.Genres.FirstOrDefault().GenreName
            }).FirstOrDefault().genre;

            _bookService.Setup(_ => _.GetByGenreServiceAsync(parameters, It.IsAny<string>())).ReturnsAsync(response);
            BookController bookController = new BookController(_bookService.Object);
            /// Act
            var result = (NotFoundObjectResult)await bookController
                .GetByGenre(parameters, new string(genre.Reverse().ToArray()));
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
