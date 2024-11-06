using System;
using System.Collections.Generic;
using System.Windows;

namespace Kursach
{
    public partial class TicketViewWindow : Window
    {
        private List<Flight> flights;
        private List<Purchase> purchases;

        public TicketViewWindow(List<Purchase> purchases)
        {
            InitializeComponent();
            this.purchases = purchases;
            LoadFlights();
        }

        private void LoadFlights()
        {
            flights = new List<Flight>
            {
                new Flight { FlightNumber = "101", InitialPoint = "Кременчук", FinalDestination = "Київ", IntermediateStops = "Полтава", DepartureTime = "08:30", FreeSeats = 15, Price = 150.00m },
                new Flight { FlightNumber = "102", InitialPoint = "Кременчук", FinalDestination = "Олександрія", IntermediateStops = "Полтава", DepartureTime = "09:00", FreeSeats = 10, Price = 120.00m },
                new Flight { FlightNumber = "103", InitialPoint = "Кременчук", FinalDestination = "Кропивницький", IntermediateStops = "Олександрія", DepartureTime = "09:30", FreeSeats = 5, Price = 130.00m },
                new Flight { FlightNumber = "104", InitialPoint = "Кременчук", FinalDestination = "Полтава", IntermediateStops = "Київ", DepartureTime = "10:00", FreeSeats = 20, Price = 110.00m },
                new Flight { FlightNumber = "105", InitialPoint = "Кременчук", FinalDestination = "Світловодськ", IntermediateStops = "Кропивницький", DepartureTime = "11:00", FreeSeats = 90, Price = 140.00m }
            };

            flights.RemoveAll(flight => flight.InitialPoint == flight.FinalDestination);
            TicketListView.ItemsSource = flights;
        }

        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {
            if (TicketListView.SelectedItem is Flight selectedFlight)
            {
                int ticketCount = 1;

                if (selectedFlight.FreeSeats >= ticketCount)
                {
                    selectedFlight.FreeSeats -= ticketCount;
                    TicketListView.Items.Refresh();

                    var newPurchase = new Purchase
                    {
                        PurchaseDate = DateTime.Now,
                        FlightNumber = selectedFlight.FlightNumber,
                        Destination = selectedFlight.FinalDestination,
                        Price = selectedFlight.Price,
                        Status = "Успішно"
                    };

                    purchases.Add(newPurchase);
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
