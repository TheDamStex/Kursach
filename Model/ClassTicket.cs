using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    public class Ticket
    {
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } // Придбаний або доступний
    }

}
