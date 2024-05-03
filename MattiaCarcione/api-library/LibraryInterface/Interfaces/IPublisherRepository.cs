using LibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInterface.Interfaces
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher> GetByIdAsync(int id);
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
        Task<bool> DeleteAsync(int id);
    }
}
