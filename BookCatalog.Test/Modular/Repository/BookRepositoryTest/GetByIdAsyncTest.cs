using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.Repository;
using BookCatalog.Domain.Entity;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Test.Helpers;
using BookCatalog.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Repository.BookRepositoryTest
{
    public class GetByIdAsyncTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private readonly IMapper? _mapper;

        public GetByIdAsyncTest()
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
        /// Проверяет что обработчик возвращает не null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdAsync_NotNullResult()
        {
            /// Arrange
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.GetByIdAsync(BookMockData.Get().FirstOrDefault().BookId);
            /// Assert
            Assert.NotNull(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный результат
        /// </summary>
        /// <param name="validId"></param>
        /// <param name="invalidId"></param>
        [Theory]
        [InlineData(1, 1)]
        public void GetByIdAsync_ReturnsRight(int validId, int invalidId)
        {
            //Arrange
            invalidId += BookMockData.Get().LastOrDefault().BookId;
            Book book = _mapper.Map<Book>(BookMockData.GetById(validId));
            BookRepository bookRep = new BookRepository(_context, _mapper);
            //Act
            var nullResult = bookRep.GetByIdAsync(invalidId);
            var entityResult = bookRep.GetByIdAsync(book.BookId);
            //Assert
            Assert.Equal(book.BookName, entityResult.Result.BookName);
            Assert.Equal(book.BookId, entityResult.Result.BookId);
            Assert.Equal(null, nullResult.Result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdAsync_ReturnsRightType()
        {
            /// Arrange
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.GetByIdAsync(BookMockData.Get().FirstOrDefault().BookId);
            /// Assert
            Assert.IsType<BookDto>(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
