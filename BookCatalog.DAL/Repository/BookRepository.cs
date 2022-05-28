using AutoMapper;
using BookCatalog.DAL.Interfaces;
using BookCatalog.Domain.Entity;
using BookCatalog.Domain.Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public BookRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<BookDto> CreateAsync(BookDto entity)
        {
            Book book = _mapper.Map<BookDto, Book>(entity);
            _db.Book.Add(book);
            await _db.SaveChangesAsync();
            return _mapper.Map<Book, BookDto>(book);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Book? book = await _db.Book.Include(genre => genre.Genres).FirstOrDefaultAsync(x => x.BookId == id);
                if (book is null)
                {
                    return false;
                }
                _db.Book.Remove(book);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<BookDto>> GetAsync() =>

            _mapper.Map<IEnumerable<BookDto>>(await _db.Book.Include(genre => genre.Genres).ToListAsync());

        public async Task<IEnumerable<BookDto>> GetByAuthorAsync(string author) =>

            _mapper.Map<IEnumerable<BookDto>>(await _db.Book.Include(genre => genre.Genres).Where(x => x.Author.ToUpper().Replace(" ", "") == author.ToUpper().Replace(" ", "")).ToListAsync());

        public async Task<BookDto> GetByIdAsync(int id) =>

            _mapper.Map<BookDto>(await _db.Book.Include(genre => genre.Genres).FirstOrDefaultAsync(x => x.BookId == id));

        public async Task<BookDto> UpdateAsync(BookDto entity)
        {
            Book book = _mapper.Map<BookDto, Book>(entity);
            if (await _db.Book.AsNoTracking().FirstOrDefaultAsync(x => x.BookId == entity.BookId) is null)
            {
                throw new NullReferenceException("Попытка обновить объект, которого нет в хранилище.");
            }
            _db.Book.Update(book);
            await _db.SaveChangesAsync();
            return _mapper.Map<Book, BookDto>(book);
        }
    }
}
