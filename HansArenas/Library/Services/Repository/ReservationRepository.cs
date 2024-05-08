using Entities.Data;
using Entities.Model;
using Interfaces.crud_entities;
using Microsoft.EntityFrameworkCore;

using Services.Exceptions;
namespace Services.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly Library_DbContext _context;
        public ReservationRepository(Library_DbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> CreateAsync(Reservation reservationModel)
        {
            await _context.AddAsync(reservationModel);
            await _context.SaveChangesAsync();
            return reservationModel;
        }

        public async Task<Reservation> CreateTestAsync(string User, Book BookToReserve)
        {
            try
            {
                //if (!((_context.Reservations.Count(p=>p.User == User && p.Return_Date == null))>=3))
                //    throw new ReservationExeption("utente ha già 3 prenotazioni aperte", ErrorCode.UserWithActiveReservation);
                //if (BookToReserve == null)
                //    throw new ReservationExeption("Libro non trovato", ErrorCode.BookNotFound);

                //if (BookToReserve.NumberOfCopiesLeft <= 0)
                //    throw new ReservationExeption("Numero di copie esaurite", ErrorCode.FinishedCopies);

                //if (_context.Reservations.Any(p=> p.BookId == BookToReserve.BookId && p.User == User))
                //    throw new ReservationExeption("L'utente ha già prenotato lo stesso libro", ErrorCode.BookAlredyReservedByUser);
                return new Reservation
                {
                    User = User,
                    Book_Reserved = BookToReserve,
                };
            }
            catch (ReservationExeption ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();

        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }
    }
}
