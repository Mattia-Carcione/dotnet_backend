using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDtos.Dtos.Author;
using LibraryDtos.Dtos.Category;
using LibraryDtos.Dtos.Publisher;
using LibraryDtos.Dtos.Booking;

namespace LibraryDtos.Dtos.Book
{
    public class BookDetailDto
    {
        public int BookID { get; set; }
        public required string Title { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? PublishingDate { get; set; }
        public required int TotalCopies { get; set; }
        public required int TotalCopiesLeft { get; set; }
        public required AuthorDto Author { get; set; }
        public List<CategoryDto>? Categories { get; set; }
        public required PublisherDto Publisher { get; set; }
    }
}
