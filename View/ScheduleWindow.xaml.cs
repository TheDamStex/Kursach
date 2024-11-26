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

namespace Kursach.View
{
    /// <summary>
    /// Логика взаимодействия для ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        public ScheduleWindow()
        {
            InitializeComponent();
            // Створюємо масив даних для відображення
            var busSchedules = new[]
            {
                new { FlightNumber = "101", InitialPoint = "Кременчук", FinalDestination = "Київ", IntermediateStops = "Полтава", DepartureTime = "08:30", FreeSeats = 15, Price = 150 },
                new { FlightNumber = "102", InitialPoint = "Кременчук", FinalDestination = "Олександрія", IntermediateStops = "Кропивницький, Полтава", DepartureTime = "09:00", FreeSeats = 10, Price = 130 },
                new { FlightNumber = "103", InitialPoint = "Кременчук", FinalDestination = "Кропивницький", IntermediateStops = "Світловодськ, Олександрія", DepartureTime = "09:30", FreeSeats = 5, Price = 120 },
                new { FlightNumber = "104", InitialPoint = "Кременчук", FinalDestination = "Полтава", IntermediateStops = "Київ", DepartureTime = "10:00", FreeSeats = 20, Price = 140 },
                new { FlightNumber = "105", InitialPoint = "Кременчук", FinalDestination = "Світловодськ", IntermediateStops = "Кропивницький", DepartureTime = "11:00", FreeSeats = 8, Price = 110 }
            };

            BusScheduleListView.ItemsSource = busSchedules; // Прив'язуємо дані до listview
        }
    }
}



