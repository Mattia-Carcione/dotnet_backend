using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDtos.Dtos.Book;

namespace LibraryDtos.Dtos.Author
{
    public class AuthorDetailDto
    {
        public int AuthorID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public List<BookDto>? Books { get; set; }
    }
}
