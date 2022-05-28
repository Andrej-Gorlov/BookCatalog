using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.Repository;
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
    public class UpdateAsyncTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private readonly IMapper? _mapper;
        public UpdateAsyncTest()
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
        public async Task UpdateAsync_ReturnsRightType()
        {
            /// Arrange
            BookDto book = new()
            {
                BookId = 3,
                BookName = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.0 на языке C#"
            };
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.UpdateAsync(book);
            /// Assert
            Assert.IsType<BookDto>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateAsync_ReturnsRight()
        {
            /// Arrange
            BookDto book = new()
            {
                BookId = 3,
                BookName = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.0 на языке C#"
            };
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.UpdateAsync(book);
            /// Assert
            Assert.Equal(result.BookName, book.BookName);
            Assert.Equal(result.BookId, book.BookId);
        }
        /// <summary>
        /// Если из БД вернулся null, проверяем на исключение
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateAsync_ExceptionReturns()
        {
            /// Arrange
            BookDto book = new()
            {
                BookId= BookMockData.Get().Count() + 1,
                BookName = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.0 на языке C#"
            };
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Assert
            await Assert.ThrowsAsync<NullReferenceException>(() =>
                /// Act
                bookRep.UpdateAsync(book));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
