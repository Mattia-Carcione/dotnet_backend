using LibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInterface.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> GetBookByBookingAsync(int id);
        Task<bool> GetBookIsReturnedAsync(int id, string user);
        Task<bool> GetBookIsSoldOutAsync(int id);
        Task<List<Book>> GetBooksByAuthorAsync(string lastName);
        Task<List<Book>> GetBooksByCategoryAsync(string categoryName);
        Task<List<Book>> GetBooksByNumbOfPagesAsync(int pageOffset, int pageLimit);
        Task<List<Book>> GetBooksByPublishingDateAsync(DateTime dateOffset, DateTime dateLimit);
        Task<List<Book>> GetBooksByTitleAsync(string title);
        Task<Book> CreateAsync(Book book);
        Task<Book> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
    }
}
