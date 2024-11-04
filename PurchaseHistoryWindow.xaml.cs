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
    /// Логика взаимодействия для PurchaseHistoryWindow.xaml
    /// </summary>
    public partial class PurchaseHistoryWindow : Window
    {
        private List<Purchase> purchases;

        public PurchaseHistoryWindow()
        {
            InitializeComponent();
            LoadPurchaseHistory();
        }

        private void LoadPurchaseHistory()
        {
            // Додаємо записи до історії покупок
            purchases = new List<Purchase>
            {
                new Purchase { PurchaseDate = DateTime.Now.AddDays(-1), FlightNumber = "101", Destination = "Київ", Price = 150.00m, Status = "Успішно" },
                new Purchase { PurchaseDate = DateTime.Now.AddDays(-2), FlightNumber = "102", Destination = "Олександрія", Price = 120.00m, Status = "Успішно" },
                new Purchase { PurchaseDate = DateTime.Now.AddDays(-3), FlightNumber = "103", Destination = "Кропивницький", Price = 130.00m, Status = "Повернуто" }
            };

            PurchaseHistoryListView.ItemsSource = purchases;
        }

        private void ReturnTicket_Click(object sender, RoutedEventArgs e)
        {
            // Логіка для повернення квитка
            MessageBox.Show("Повернення квитка не реалізовано.", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
