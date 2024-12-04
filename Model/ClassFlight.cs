namespace Kursach
{
    // Клас, що описує рейс
    public class Flight
    {
        public int UserId { get; set; } // ID користувача (для асоціації з користувачем)
        public string FlightNumber { get; set; } // Номер рейсу
        public string InitialPoint { get; set; } // Початковий пункт подорожі
        public string FinalDestination { get; set; } // Кінцевий пункт подорожі
        public string IntermediateStops { get; set; } // Проміжні зупинки (якщо є)
        public string DepartureTime { get; set; } // Час відправлення рейсу
        public int FreeSeats { get; set; } // Кількість вільних місць
        public decimal Price { get; set; } // Ціна квитка
    }
}
