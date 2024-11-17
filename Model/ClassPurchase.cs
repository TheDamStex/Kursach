using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    public class Purchase
    {
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
