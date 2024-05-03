using LibraryDtos.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDtos.Dtos.Category
{
    public class CategoryDetailDto
    {
        public int CategoryID { get; set; }
        public required string Genre { get; set; }
        public string? Description { get; set; }
        public List<BookDto>? Books { get; set; }
    }
}
