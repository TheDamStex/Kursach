using System;

namespace Kursach.Model
{
    // Клас, що описує квиток користувача
    public class UserTicket
    {
        public int UserId { get; set; } // Унікальний ідентифікатор користувача
        public string FlightNumber { get; set; } // Номер рейсу
        public string InitialPoint { get; set; } // Початковий пункт рейсу
        public string FinalDestination { get; set; } // Кінцевий пункт рейсу
        public string DepartureTime { get; set; } // Час відправлення рейсу
        public decimal Price { get; set; } // Ціна квитка
        public DateTime PurchaseDate { get; set; } // Дата покупки квитка

        // Нові властивості для відображення
        public string Destination => FinalDestination; // Прив'язуємо до існуючого властивості кінцевого пункту
        public string Status => "Покупка успішна"; // Статус можна задати статичним або динамічним
    }
}
