using Kursach;
using Kursach.Services;
using Kursach.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;



public class TicketViewViewModel : ViewModelBase
{
    public ICommand OpenPaymentWindowCommand { get; }
    public ObservableCollection<Flight> Flights { get; private set; }

    private Flight _selectedFlight;
    public Flight SelectedFlight
    {
        get => _selectedFlight;
        set
        {
            _selectedFlight = value;
            OnPropertyChanged(nameof(SelectedFlight));
            ((RelayCommand)OpenPaymentWindowCommand).RaiseCanExecuteChanged();
        }
    }

    public TicketViewViewModel()
    {

        // Инициализация данных рейсов
        Flights = new ObservableCollection<Flight>
        {
            new Flight { FlightNumber = "101", InitialPoint = "Кременчук", FinalDestination = "Київ", DepartureTime = "08:30", FreeSeats = 15, Price = 150.00m },
            new Flight { FlightNumber = "102", InitialPoint = "Кременчук", FinalDestination = "Олександрія", DepartureTime = "09:00", FreeSeats = 10, Price = 120.00m },
            new Flight { FlightNumber = "103", InitialPoint = "Кременчук", FinalDestination = "Кропивницький", DepartureTime = "09:30", FreeSeats = 5, Price = 130.00m },
            new Flight { FlightNumber = "104", InitialPoint = "Кременчук", FinalDestination = "Полтава", DepartureTime = "10:00", FreeSeats = 20, Price = 110.00m }
        };

        OpenPaymentWindowCommand = new RelayCommand(ExecuteOpenPaymentWindowCommand);
    }

    // Метод, проверяющий возможность открытия окна оплаты (если есть выбранный рейс и свободные места)
    private bool CanExecuteOpenPaymentWindowCommand()
    {
        return SelectedFlight != null && SelectedFlight.FreeSeats > 0;
    }

    // Метод, открывающий окно оплаты и выполняющий логику покупки билета
    private void ExecuteOpenPaymentWindowCommand()
    {
        if (SelectedFlight != null)
        {
            // Открытие окна оплаты
            PaymentWindow paymentWindow = new PaymentWindow();
            paymentWindow.Show();

            // Уменьшение количества свободных мест
            SelectedFlight.FreeSeats--;

            // Сохранение билета
            SaveTicketForUser(SelectedFlight);

            // Уведомляем об изменении данных
            OnPropertyChanged(nameof(Flights));
        }
        else
        {
            MessageBox.Show("Пожалуйста, выберите рейс для покупки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    // Метод для сохранения данных о покупке билета
    private void SaveTicketForUser(Flight flight)
    {
        // ID текущего пользователя (пример, нужно связать с логикой авторизации)
        int currentUserId = AuthService.CurrentUserId;

        // Создаем билет
        Ticket ticket = new Ticket
        {
            FlightNumber = flight.FlightNumber,
            DepartureTime = flight.DepartureTime,
            Price = flight.Price
        };

        // Загружаем существующие билеты
        var tickets = new List<Ticket>();
        if (File.Exists("tickets.json"))
        {
            string json = File.ReadAllText("tickets.json");
            if (!string.IsNullOrWhiteSpace(json))
            {
                tickets = JsonSerializer.Deserialize<List<Ticket>>(json);
            }
        }

        // Добавляем новый билет
        tickets.Add(ticket);

        // Сохраняем обновленные данные в JSON
        string updatedJson = JsonSerializer.Serialize(tickets);
        File.WriteAllText("tickets.json", updatedJson);
    }

}
