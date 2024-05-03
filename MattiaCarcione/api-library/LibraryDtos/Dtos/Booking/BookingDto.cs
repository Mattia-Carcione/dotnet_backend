using LibraryDtos.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDtos.Dtos.Booking
{
    public class BookingDto
    {
        public int BookingID { get; set; }
        public required string User { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public required BookDto Book { get; set; }
    }
}
