using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для TicketViewWindow.xaml
    /// </summary>
    public partial class TicketViewWindow : Window
    {
        private List<Flight> flights;

        public TicketViewWindow()
        {
            InitializeComponent();
            LoadFlights();
        }

        private void LoadFlights()
        {
            // Додаємо рейси до списку, з "Кременчук" як початковий пункт
            flights = new List<Flight>
            {
                new Flight { FlightNumber = "101", InitialPoint = "Кременчук", FinalDestination = "Київ", IntermediateStops = "Полтава", DepartureTime = "08:30", FreeSeats = 15, Price = 150.00m },
                new Flight { FlightNumber = "102", InitialPoint = "Кременчук", FinalDestination = "Олександрія", IntermediateStops = "Полтава", DepartureTime = "09:00", FreeSeats = 10, Price = 120.00m },
                new Flight { FlightNumber = "103", InitialPoint = "Кременчук", FinalDestination = "Кропивницький", IntermediateStops = "Олександрія", DepartureTime = "09:30", FreeSeats = 5, Price = 130.00m },
                new Flight { FlightNumber = "104", InitialPoint = "Кременчук", FinalDestination = "Полтава", IntermediateStops = "Київ", DepartureTime = "10:00", FreeSeats = 20, Price = 110.00m },
                new Flight { FlightNumber = "105", InitialPoint = "Кременчук", FinalDestination = "Світловодськ", IntermediateStops = "Кропивницький", DepartureTime = "11:00", FreeSeats = 90, Price = 140.00m }
            };

            // Фільтруємо рейси, щоб не було збігу початкового пункту з кінцевим
            flights.RemoveAll(flight => flight.InitialPoint == flight.FinalDestination);

            TicketListView.ItemsSource = flights;
        }

        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {
            // Вибір рейсу з ListView
            if (TicketListView.SelectedItem is Flight selectedFlight)
            {
                // Кількість квитків, яку хоче купити користувач
                int ticketCount = 1; // Можна отримати з діалогового вікна або текстового поля, якщо додати таке

                // Перевірка наявності вільних місць
                if (selectedFlight.FreeSeats >= ticketCount)
                {
                    // Зменшуємо кількість вільних місць
                    selectedFlight.FreeSeats -= ticketCount;

                    // Оновлюємо ListView
                    TicketListView.Items.Refresh();
                    MessageBox.Show($"Ви успішно купили {ticketCount} квиток(и) на рейс {selectedFlight.FlightNumber}!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("На жаль, недостатньо вільних місць для купівлі!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть рейс для купівлі квитка.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}