using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    public class Flight
    {
        public int UserId { get; set; } // ID пользователя
        public string FlightNumber { get; set; }
        public string InitialPoint { get; set; } // Початковий пункт
        public string FinalDestination { get; set; }
        public string IntermediateStops { get; set; }
        public string DepartureTime { get; set; }
        public int FreeSeats { get; set; }
        public decimal Price { get; set; } // Ціна
    }
}
