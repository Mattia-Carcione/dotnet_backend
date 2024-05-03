using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDtos.Dtos.Author
{
    public class AuthorDto
    {
        public int AuthorID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
