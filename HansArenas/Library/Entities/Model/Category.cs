using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //FK

        public List<Book> LibriInCategoria { get; set; } = new List<Book>();
    }
}
