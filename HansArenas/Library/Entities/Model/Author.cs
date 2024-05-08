using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Author_Name { get; set; }
        public string Author_Surname { get; set; }
        public DateOnly Author_DateOfBirthhday { get; set; }
        //FK
        public List<Book> Author_WrittenBooks { get; set; } = new List<Book>();
    }
}
