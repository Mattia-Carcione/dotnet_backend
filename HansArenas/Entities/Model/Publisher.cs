using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Publisher_Name { get; set; }

        //FK
        public List<Book> Publisher_Books { get; set; } = new List<Book>();
    }
}
