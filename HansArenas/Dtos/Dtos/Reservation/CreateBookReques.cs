using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Reservations
{
    public class CreateBookRequest
    {
        public string User { get; set; }
        public string Title { get; set; }
    }

}
