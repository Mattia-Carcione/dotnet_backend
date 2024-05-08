using Intefaces.crud.entities;
using Entities.Data;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Dtos.Author;

namespace Services.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly Library_DbContext _context;
        public AuthorRepository(Library_DbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }


        public async Task<Author?> CreateAsync(Author authorModel)
        {
            await _context.Authors.AddAsync(authorModel);
            await _context.SaveChangesAsync();
            return authorModel;
        }

        public async Task<Author?> DeleteAsync(int id)
        {
            var authorModel = await _context.Authors.FindAsync(id);
            if (authorModel == null)
            {
                return null;
            }
            _context.Authors.Remove(authorModel);
            await _context.SaveChangesAsync();
            return authorModel;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<Author?> UpdateAsync(int id, UpdateAuthorRequestDto authorDto)
        {
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == id);
            if (existingAuthor == null)
            {
                return null;
            }
            
            existingAuthor.Author_Name = authorDto.Author_Name;
            existingAuthor.Author_Surname = authorDto.Author_Surname;
            existingAuthor.Author_DateOfBirthhday = authorDto.Author_DateOfBirthhday;
            await _context.SaveChangesAsync();
            return existingAuthor;
        }
        

        
    }
}
