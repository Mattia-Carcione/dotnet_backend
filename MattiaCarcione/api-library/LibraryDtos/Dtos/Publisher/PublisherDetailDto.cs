using LibraryDtos.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDtos.Dtos.Publisher
{
    public class PublisherDetailDto
    {
        public int PublisherID { get; set; }
        public required string Name { get; set; }
        public List<BookDto>? Books { get; set; }
    }
}
