using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDtos.Dtos.Category
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public required string Genre { get; set; }
        public string? Description { get; set; }
    }
}
