using Dtos.Author;
using Entities.Model;

namespace Intefaces.crud.entities
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author?> CreateAsync(Author authorModel);
        Task<Author?> UpdateAsync(int id, UpdateAuthorRequestDto authorDto);
        Task<Author?> DeleteAsync(int id);
        
    }
}
