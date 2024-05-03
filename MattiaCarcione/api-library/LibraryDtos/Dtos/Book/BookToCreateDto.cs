using LibraryDtos.Dtos.Author;
using LibraryDtos.Dtos.Category;
using LibraryDtos.Dtos.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDtos.Dtos.Book
{
    public class BookToCreateDto
    {
        public int BookID { get; set; }
        public required string Title { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? PublishingDate { get; set; }
        public required int TotalCopies { get; set; }
        public required int TotalCopiesLeft { get; set; }
        public int AuthorID { get; set; }
        public int PublisherID { get; set; }
    }
}
