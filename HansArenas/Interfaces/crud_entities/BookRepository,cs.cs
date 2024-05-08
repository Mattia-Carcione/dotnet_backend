using Entities.Model;
using Dtos.BookDtos;


namespace Intefaces.crud.entities
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> CreateAsync(Book bookModel);
        Task<Book?> UpdateAsync(int id, UpdateBookRequestDto updateBookRequestDto);
        Task<Book?> DeleteAsync(int id);
        Task<Book?> GetByTitleAsync(string Title);
    }
}
