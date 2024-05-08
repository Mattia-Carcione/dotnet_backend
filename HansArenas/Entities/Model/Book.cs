using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;

        public int NumberOfPages { get; set; }
        public DateOnly DateOfPublication { get; set; }
        public int NumberOfTotalCopies { get; set; }
        public int NumberOfCopiesLeft { get; set; }

        //FK
        public int? Publisher_Id { get; set; }
        public Publisher? Publisher { get; set; }
        public int? Author_Id { get; set; }
        public Author? Author { get; set; }

        public List<Category> Categories_InTheBook { get; set; } = new List<Category>();
        public List<Reservation> Bookings { get; set; } = new List<Reservation>();



    }
}
