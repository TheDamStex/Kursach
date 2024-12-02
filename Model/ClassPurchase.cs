using System;

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
