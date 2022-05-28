using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.Repository;
using BookCatalog.Test.Helpers;
using BookCatalog.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookCatalog.Test.Modular.Repository.BookRepositoryTest
{
    public class DeleteAsyncTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private readonly IMapper? _mapper;

        public DeleteAsyncTest()
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
        /// Проверяет что обработчик возвращает true 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteAsync_ReturnsTrue()
        {
            /// Arrange
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.DeleteAsync(BookMockData.Get().FirstOrDefault().BookId);
            /// Assert
            Assert.True(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает false 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteAsync_ReturnsFalse()
        {
            /// Arrange
            BookRepository bookRep = new BookRepository(_context, _mapper);
            /// Act
            var result = await bookRep.DeleteAsync(BookMockData.Get().Count() + 1);
            /// Assert
            Assert.False(result);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
