using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.Repository;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Test.Helpers;
using BookCatalog.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Repository.BookRepositoryTest
{
    public class GetByAuthorAsyncTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private readonly IMapper? _mapper;
        public GetByAuthorAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();

            if (_mapper is null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new SourceMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGames_ReturnsRightType()
        {
            /// Arrange
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.GetByAuthorAsync(BookMockData.Get().LastOrDefault().Author);
            /// Assert
            Assert.IsType<List<BookDto>>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGames_ShouldReturnCorrect()
        {
            /// Arrange
            List<BookDto> books = BookMockData.Get().Where(x=>x.Author== BookMockData.Get().FirstOrDefault().Author).ToList();
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.GetByAuthorAsync(BookMockData.Get().FirstOrDefault().Author);
            /// Assert
            Assert.NotNull(result);
            Assert.Equal(books.Count(), result.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
