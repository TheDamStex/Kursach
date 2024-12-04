using Kursach.View;
using Kursach;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Kursach.ViewModel;
using Kursach.Services;
using Kursach.Model;

public class TicketViewViewModel : ViewModelBase
{
    // Команда для відкриття вікна оплати
    public ICommand OpenPaymentWindowCommand { get; }

    // Колекція рейсів, доступних для покупки квитків
    public ObservableCollection<Flight> Flights { get; private set; }

    // Вибраний рейс для покупки квитка
    private Flight _selectedFlight;

    public Flight SelectedFlight
    {
        get => _selectedFlight;
        set
        {
            _selectedFlight = value;
            OnPropertyChanged(nameof(SelectedFlight)); // Сповіщаємо про зміну вибраного рейсу
            ((RelayCommand)OpenPaymentWindowCommand).RaiseCanExecuteChanged(); // Оновлюємо можливість виконання команди
        }
    }

    // Конструктор класу
    public TicketViewViewModel()
    {
        Flights = new ObservableCollection<Flight>();
        LoadFlightsFromFile(); // Завантажуємо рейси з файлу

        // Ініціалізація команди для відкриття вікна оплати
        OpenPaymentWindowCommand = new RelayCommand(ExecuteOpenPaymentWindowCommand, CanExecuteOpenPaymentWindowCommand);
    }

    // Завантаження рейсів з файлу
    private void LoadFlightsFromFile()
    {
        const string flightsFilePath = "flights.json";

        if (File.Exists(flightsFilePath))
        {
            try
            {
                var flightsFromFile = JsonSerializer.Deserialize<List<Flight>>(File.ReadAllText(flightsFilePath));
                if (flightsFromFile != null)
                {
                    Flights = new ObservableCollection<Flight>(flightsFromFile); // Заповнюємо колекцію рейсами
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні даних рейсів: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            MessageBox.Show("Файл з рейсами не знайдений. Перевірте, чи існує файл flights.json.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    // Перевірка, чи можна виконати команду відкриття вікна оплати
    private bool CanExecuteOpenPaymentWindowCommand()
    {
        return SelectedFlight != null && SelectedFlight.FreeSeats > 0; // Команда активна, якщо обрано рейс і є вільні місця
    }

    // Метод виконання команди для відкриття вікна оплати
    private void ExecuteOpenPaymentWindowCommand()
    {
        if (SelectedFlight != null)
        {
            var paymentViewModel = new PaymentViewModel();

            // Підписка на подію успішної оплати
            paymentViewModel.PaymentSuccessful += () =>
            {
                ProcessTicketPurchase(); // Обробка покупки квитка
                MessageBox.Show("Квиток успішно куплено!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            };

            // Створення вікна оплати і прив'язка його до ViewModel
            PaymentWindow paymentWindow = new PaymentWindow
            {
                DataContext = paymentViewModel
            };

            paymentWindow.ShowDialog(); // Відкриваємо вікно оплати
        }
        else
        {
            MessageBox.Show("Будь ласка, виберіть рейс для покупки.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    // Обробка покупки квитка
    private void ProcessTicketPurchase()
    {
        if (SelectedFlight == null || SelectedFlight.FreeSeats <= 0)
        {
            MessageBox.Show("Немає доступних квитків для покупки.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Зменшуємо кількість вільних місць на рейсі
        SelectedFlight.FreeSeats--;

        // Оновлюємо файл рейсів
        UpdateTicketsFile();

        // Додаємо квиток у файл користувача
        AddTicketToUserFile();

        // Сповіщаємо інтерфейс про зміни
        OnPropertyChanged(nameof(Flights));
        OnPropertyChanged(nameof(SelectedFlight));
    }

    // Оновлення файлу рейсів після покупки квитка
    private void UpdateTicketsFile()
    {
        const string ticketsFilePath = "flights.json";

        List<Flight> flights = File.Exists(ticketsFilePath)
            ? JsonSerializer.Deserialize<List<Flight>>(File.ReadAllText(ticketsFilePath)) ?? new List<Flight>()
            : new List<Flight>(Flights);

        // Шукаємо рейс для оновлення
        var flightToUpdate = flights.FirstOrDefault(f => f.FlightNumber == SelectedFlight.FlightNumber);

        if (flightToUpdate != null)
        {
            flightToUpdate.FreeSeats = SelectedFlight.FreeSeats; // Оновлюємо кількість вільних місць
        }

        // Зберігаємо оновлені дані в файл
        File.WriteAllText(ticketsFilePath, JsonSerializer.Serialize(flights));
    }

    // Додавання квитка в файл користувача
    private void AddTicketToUserFile()
    {
        const string userTicketsFilePath = "user_tickets.json";

        List<UserTicket> userTickets = File.Exists(userTicketsFilePath)
            ? JsonSerializer.Deserialize<List<UserTicket>>(File.ReadAllText(userTicketsFilePath)) ?? new List<UserTicket>()
            : new List<UserTicket>();

        // Створюємо новий квиток для користувача
        var userTicket = new UserTicket
        {
            UserId = AuthService.CurrentUserId, // ID поточного користувача
            FlightNumber = SelectedFlight.FlightNumber,
            InitialPoint = SelectedFlight.InitialPoint,
            FinalDestination = SelectedFlight.FinalDestination,
            DepartureTime = SelectedFlight.DepartureTime,
            Price = SelectedFlight.Price,
            PurchaseDate = DateTime.Now // Дата покупки
        };

        // Додаємо квиток у файл і зберігаємо дані
        userTickets.Add(userTicket);
        File.WriteAllText(userTicketsFilePath, JsonSerializer.Serialize(userTickets));
    }
}
