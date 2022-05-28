using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.Repository;
using BookCatalog.Domain.Entity;
using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Test.Helpers;
using BookCatalog.Test.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Repository.BookRepositoryTest
{
    public class CreateAsyncTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private readonly IMapper? _mapper;
        public CreateAsyncTest()
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
        /// Проверяет что обработчик возвращает правильное количество записей в бд 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAsync_RightRecordCountToDb()
        {
            /// Arrange
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.CreateAsync(BookMockData.Model());
            /// Assert
            int expectedRecordCount = BookMockData.Get().Count() + 1;
            _context.Book.Count().Should().Be(expectedRecordCount);
        }
        /// <summary>
        /// Обработчик проверяет последнюю книгу в бд
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAsync_ReturnsLastEntityToDb()
        {
            /// Arrange
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = _mapper.Map<Book>(await bookRep.CreateAsync(BookMockData.Model()));
            var model = _context.Book.LastOrDefault();
            /// Assert
            Assert.Equal(result.BookName, model.BookName);
            Assert.Equal(result.BookId, model.BookId);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAsync_ReturnsRightType()
        {
            /// Arrange
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.CreateAsync(BookMockData.Model());
            /// Assert
            Assert.IsType<BookDto>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает новую книгу
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAsync_ReturnsNewReview()
        {
            /// Arrange
            BookDto book = BookMockData.Model();
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.CreateAsync(book);
            /// Assert
            Assert.Equal(book.BookName, result.BookName);
            Assert.Equal(book.BookId, result.BookId);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
