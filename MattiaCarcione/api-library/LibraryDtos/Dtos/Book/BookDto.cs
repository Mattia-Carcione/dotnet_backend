using LibraryDtos.Dtos.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDtos.Dtos.Book
{
    public class BookDto
    {
        public int BookID { get; set; }
        public required string Title { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? PublishingDate { get; set; }
        public required int TotalCopies { get; set; }
        public required int TotalCopiesLeft { get; set; }
    }
}
