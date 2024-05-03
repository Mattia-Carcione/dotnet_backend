using LibraryInterface.Interfaces;
using LibraryModel.Model;
using LibraryServices.Services.Create;
using LibraryServices.Services.Update;
using LibraryServices.Services.Delete;
using LibraryServices.Services.Read.ReadBook;

namespace LibraryRepository.Repositories
{
    public class BookRepository : IBookRepository
    {
        public async Task<List<Book>> GetAllAsync()
        {
            var books = await ReadBooks.GetAllBooks();

            return books;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var book = await ReadGetBookById.GetBookById(id);

            return book;
        }

        public async Task<Book> GetBookByBookingAsync(int id)
        {
            var book = await ReadBookByBooking.SearchBookByBooking(id);

            return book;
        }

        public async Task<bool> GetBookIsReturnedAsync(int id, string user)
        {
            var book = await ReadBookIsReturned.IsBookReturned(id, user);

            return book;
        }

        public async Task<bool> GetBookIsSoldOutAsync(int id)
        {
            var book = await ReadBookIsSoldOut.IsBookSoldOut(id);

            return book;
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(string lastName)
        {
            var book = await ReadBooksByAuthor.SearchBooksByAuthor(lastName);

            return book;
        }

        public async Task<List<Book>> GetBooksByCategoryAsync(string categoryName)
        {
            var book = await ReadBooksByCategory.SearchBooksByCategories(categoryName);

            return book;
        }

        public async Task<List<Book>> GetBooksByNumbOfPagesAsync(int pageOffset, int pageLimit)
        {
            var book = await ReadBooksByNumbOfPages.SearchBooksByNumberOfPages(pageOffset, pageLimit);

            return book;
        }

        public async Task<List<Book>> GetBooksByPublishingDateAsync(DateTime dateOffset, DateTime dateLimit)
        {
            var book = await ReadBooksByPublishingDate.SearchBooksByPublishingDate(dateOffset, dateLimit);

            return book;
        }

        public async Task<List<Book>> GetBooksByTitleAsync(string title)
        {
            var book = await ReadBooksByTitle.SearchBooksByTitle(title);

            return book;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await CreateBook.AddBook(book);

            return book;
        }

        public async Task<Book> UpdateAsync(int id, Book book)
        {
            if (book.BookID == id)
            {
                await UpdateBook.EditBook(book);

                var response = await GetByIdAsync(id);

                return response;
            }

            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await ReadGetBookById.GetBookById(id);

            await DeleteBook.RemoveBook(id);

            return true;
        }
    }
}
