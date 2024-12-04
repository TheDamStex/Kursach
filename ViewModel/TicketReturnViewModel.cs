using Kursach;
using Kursach.Model;
using Kursach.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

public class TicketReturnViewModel : ViewModelBase
{
    // Колекція квитків користувача
    public ObservableCollection<UserTicket> UserTickets { get; private set; }

    // Вибраний квиток для повернення
    private UserTicket _selectedTicket;

    // Властивість для вибору квитка
    public UserTicket SelectedTicket
    {
        get => _selectedTicket;
        set
        {
            _selectedTicket = value;
            OnPropertyChanged(nameof(SelectedTicket)); // Сповіщаємо про зміну вибраного квитка
            ((RelayCommand)ReturnTicketCommand).RaiseCanExecuteChanged(); // Оновлюємо можливість виконання команди повернення
        }
    }

    // Команда для повернення квитка
    public ICommand ReturnTicketCommand { get; }

    // Конструктор, ініціалізує команду та завантажує квитки
    public TicketReturnViewModel()
    {
        LoadUserTickets(); // Завантажуємо квитки користувача
        ReturnTicketCommand = new RelayCommand(ExecuteReturnTicket, CanExecuteReturnTicket); // Ініціалізація команди повернення квитка
    }

    // Метод для завантаження квитків користувача
    private void LoadUserTickets()
    {
        const string userTicketsFilePath = "user_tickets.json";

        // Перевірка наявності файлу з квитками
        if (File.Exists(userTicketsFilePath))
        {
            var allTickets = JsonSerializer.Deserialize<List<UserTicket>>(File.ReadAllText(userTicketsFilePath)) ?? new List<UserTicket>();

            // Фільтруємо квитки для поточного користувача
            UserTickets = new ObservableCollection<UserTicket>(allTickets.Where(t => t.UserId == AuthService.CurrentUserId));
        }
        else
        {
            // Якщо файлу немає, ініціалізуємо порожню колекцію
            UserTickets = new ObservableCollection<UserTicket>();
        }
    }

    // Перевірка, чи можна виконати команду повернення квитка
    private bool CanExecuteReturnTicket()
    {
        return SelectedTicket != null; // Команда активна, якщо обраний квиток
    }

    // Метод виконання команди повернення квитка
    private void ExecuteReturnTicket()
    {
        if (SelectedTicket == null)
        {
            MessageBox.Show("Виберіть квиток для повернення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Видаляємо квиток з файлу
        RemoveTicketFromFile();

        // Повертаємо місце на рейс
        ReturnSeatToFlight();

        // Видаляємо квиток зі списку в інтерфейсі
        UserTickets.Remove(SelectedTicket);

        // Сповіщаємо про успішне повернення
        MessageBox.Show("Квиток успішно повернено!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    // Видалення квитка з файлу
    private void RemoveTicketFromFile()
    {
        const string userTicketsFilePath = "user_tickets.json";

        var allTickets = JsonSerializer.Deserialize<List<UserTicket>>(File.ReadAllText(userTicketsFilePath)) ?? new List<UserTicket>();

        // Шукаємо квиток для видалення
        var ticketToRemove = allTickets.FirstOrDefault(t => t.FlightNumber == SelectedTicket.FlightNumber && t.UserId == SelectedTicket.UserId);

        if (ticketToRemove != null)
        {
            allTickets.Remove(ticketToRemove); // Видаляємо квиток
            File.WriteAllText(userTicketsFilePath, JsonSerializer.Serialize(allTickets)); // Оновлюємо файл
        }
    }

    // Повертаємо місце на рейс
    private void ReturnSeatToFlight()
    {
        const string flightsFilePath = "flights.json";

        var flights = JsonSerializer.Deserialize<List<Flight>>(File.ReadAllText(flightsFilePath)) ?? new List<Flight>();

        // Шукаємо рейс, до якого належить квиток
        var flight = flights.FirstOrDefault(f => f.FlightNumber == SelectedTicket.FlightNumber);

        if (flight != null)
        {
            flight.FreeSeats++; // Повертаємо місце на рейс
            File.WriteAllText(flightsFilePath, JsonSerializer.Serialize(flights)); // Оновлюємо файл з рейсами
        }
    }
}
