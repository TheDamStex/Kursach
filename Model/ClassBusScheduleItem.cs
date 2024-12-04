namespace Kursach
{
    // Клас, що описує інформацію про рейс автобуса
    public class BusScheduleItem
    {
        public string RouteNumber { get; set; } // Номер маршруту автобуса
        public string Destination { get; set; } // Кінцева зупинка маршруту
        public string IntermediateStops { get; set; } // Проміжні зупинки на маршруті
        public string DepartureTime { get; set; } // Час відправлення автобуса
        public int AvailableSeats { get; set; } // Кількість доступних місць
    }
}
