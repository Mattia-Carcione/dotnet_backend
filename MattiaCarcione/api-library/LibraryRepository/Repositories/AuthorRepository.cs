using LibraryInterface.Interfaces;
using LibraryModel.Model;
using LibraryServices.Services.Create;
using LibraryServices.Services.Update;
using LibraryServices.Services.Delete;
using LibraryServices.Services.Read.ReadAuthor;

namespace LibraryRepository.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public async Task<List<Author>> GetAllAsync()
        {
            var authors = await ReadAuthors.GetAllAuthors();

            return authors;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var response = await ReadGetAuthorById.GetAuthorById(id);

            return response;
        }
        public async Task<Author> CreateAsync(Author author)
        {
            await CreateAuthor.AddAuthor(author);

            return author;
        }
        public async Task<Author> UpdateAsync(int id, Author author)
        {
            if(author.AuthorID == id)
            {
                await UpdateAuthor.EditAuthor(author);

                var response = await GetByIdAsync(id);

                return response;
            }
            return author;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            await ReadGetAuthorById.GetAuthorById(id);

            await DeleteAuthor.RemoveAuthor(id);

            return true;
        }
    }
}
