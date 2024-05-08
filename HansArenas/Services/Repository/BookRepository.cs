
using Dtos.BookDtos;
using Intefaces.crud.entities;
using Entities.Data;
using Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Services.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly Library_DbContext _context;
        public BookRepository(Library_DbContext context) 
        {
            _context = context; 
        }

        public async Task<Book?> CreateAsync(Book bookModel)
        {
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var bookModel = await _context.Books.FindAsync(id);
            if (bookModel == null)
            {
                return null;
            }
            _context.Books.Remove(bookModel);
            await _context.SaveChangesAsync(); 
            return bookModel;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return  await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book?> GetByTitleAsync(string Title)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Title == Title);
        }

        public async Task<Book?> UpdateAsync(int id, UpdateBookRequestDto bookDto)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(x => x.BookId == id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = bookDto.Title;
            existingBook.NumberOfPages = bookDto.NumberOfPages;
            existingBook.DateOfPublication = bookDto.DateOfPublication;
            existingBook.NumberOfCopiesLeft = bookDto.NumberOfCopiesLeft;
            await _context.SaveChangesAsync();
            return existingBook;
        }
       
    }
}
