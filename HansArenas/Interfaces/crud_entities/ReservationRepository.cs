using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.crud_entities
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllAsync();
        Task<Reservation> CreateAsync(Reservation reservationModel );
        Task<Reservation?> GetByIdAsync(int id);
        Task<Reservation?> CreateTestAsync(string User, Book BookToReserve);

    }
}
