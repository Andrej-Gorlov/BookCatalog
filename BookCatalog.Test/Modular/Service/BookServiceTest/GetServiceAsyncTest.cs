using BookCatalog.DAL.Interfaces;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Paging;
using BookCatalog.Domain.Response;
using BookCatalog.Service.Implementations;
using BookCatalog.Test.MockData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Service.BookServiceTest
{
    public class GetServiceAsyncTest
    {
        private readonly Mock<IBookRepository> _bookRepository = new Mock<IBookRepository>();

        /// <summary>
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetServiceAsync_ShouldReturnCorrect()
        {
            /// Arrange
            PagingQueryParameters paging = new();
            BaseResponse<PagedList<BookDto>> response = new();
            response.Result = PagedList<BookDto>.ToPagedList(BookMockData.Get(), BookMockData.pageNumber, BookMockData.pageSize);

            _bookRepository.Setup(_ => _.GetAsync()).ReturnsAsync(response.Result);
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = await bookService.GetServiceAsync(paging);
            /// Assert
            var viewResult = Assert.IsType<BaseResponse<PagedList<BookDto>>>(result);
            var model = Assert.IsAssignableFrom<PagedList<BookDto>>(viewResult.Result);
            Assert.Equal(BookMockData.Get().Count(), model.Count());
        }
        /// <summary>
        /// Проверяет что обработчик возвращает NotNoll
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetServiceAsync_NotNoll()
        {
            /// Arrange
            PagingQueryParameters paging = new();
            BaseResponse<PagedList<BookDto>> response = new();
            response.Result = PagedList<BookDto>.ToPagedList(BookMockData.Get(), BookMockData.pageNumber, BookMockData.pageSize);

            _bookRepository.Setup(_ => _.GetAsync()).ReturnsAsync(response.Result);
            BookService bookService = new BookService(_bookRepository.Object);
            /// Act
            var result = await bookService.GetServiceAsync(paging);
            /// Assert
            Assert.NotNull(result.Result);
        }
    }
}
