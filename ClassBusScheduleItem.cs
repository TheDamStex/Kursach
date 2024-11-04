using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    public class BusScheduleItem
    {
        public string RouteNumber { get; set; }
        public string Destination { get; set; }
        public string IntermediateStops { get; set; }
        public string DepartureTime { get; set; }
        public int AvailableSeats { get; set; }
    }
}
