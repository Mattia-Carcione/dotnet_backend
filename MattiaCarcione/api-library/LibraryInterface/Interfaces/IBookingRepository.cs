using LibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInterface.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task<List<Booking>> GetBookingByUserAsync();
        Task<string> GetUserAsync();
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking> UpdateAsync(int id, Booking booking);
        Task<bool> DeleteAsync(int id);
    }
}
