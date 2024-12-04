namespace Kursach
{
    // Клас, що описує квиток на рейс
    public class Ticket
    {
        public string FlightNumber { get; set; } // Номер рейсу
        public string Destination { get; set; } // Кінцева зупинка маршруту
        public string DepartureTime { get; set; } // Час відправлення рейсу
        public int AvailableSeats { get; set; } // Кількість доступних місць на рейсі
        public decimal Price { get; set; } // Ціна квитка
        public string Status { get; set; } // Статус квитка (може бути "Придбаний" або "Доступний")
    }
}
