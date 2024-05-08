using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string User { get; set; } = string.Empty;
        public DateOnly Reservation_Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly Return_Date { get; set; }
        //FK
        public int? BookId { get; set; }
        public Book Book_Reserved { get; set; } 
    }
}
