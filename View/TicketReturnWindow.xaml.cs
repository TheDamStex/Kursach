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
    /// Логика взаимодействия для TicketReturnWindow.xaml
    /// </summary>
    public partial class TicketReturnWindow : Window
    {
        private List<Purchase> purchases;

        public TicketReturnWindow(List<Purchase> purchases)
        {
            InitializeComponent();
            this.purchases = purchases; // Приймаємо список покупок
            LoadTickets(); // Завантажуємо квитки
        }

        private void LoadTickets()
        {
            // Заповнюємо комбобокс списком квитків
            foreach (var purchase in purchases)
            {
                TicketComboBox.Items.Add($"{purchase.FlightNumber} - {purchase.Destination} ({purchase.PurchaseDate.ToShortDateString()})");
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            if (TicketComboBox.SelectedItem != null)
            {
                // Логіка повернення обраного квитка
                MessageBox.Show($"Квиток '{TicketComboBox.SelectedItem}' повернуто.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                // Тут можна додати логіку для фактичного повернення квитка в purchases
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть квиток для повернення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}