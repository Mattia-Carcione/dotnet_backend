using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.ReservationDtos
{
    public class CreateReservationRequestDto
    {
        public int ReservationId { get; set; }
        public string User { get; set; } = string.Empty;
        public DateOnly Reservation_Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly Return_Date { get; set; }
        //FK
        public int? BookId { get; set; }
    }
}
