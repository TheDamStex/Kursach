using System.Collections.Generic;
using System;
using System.Windows;

namespace Kursach
{
    public partial class MainWindow : Window
    {
        private List<Purchase> purchases = new List<Purchase>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenScheduleWindow(object sender, RoutedEventArgs e)
        {
            ScheduleWindow scheduleWindow = new ScheduleWindow();
            scheduleWindow.Show();
        }

        private void OpenLoginWindow(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }

        private void OpenTicketViewWindow(object sender, RoutedEventArgs e)
        {
            TicketViewWindow ticketViewWindow = new TicketViewWindow(purchases);
            ticketViewWindow.ShowDialog(); // Використовуємо ShowDialog для модального вікна
        }

        private void OpenPurchaseHistoryWindow(object sender, RoutedEventArgs e)
        {
            PurchaseHistoryWindow purchaseHistoryWindow = new PurchaseHistoryWindow(purchases);
            purchaseHistoryWindow.Show();
        }

        private void OpenTicketDialogWindow()
        {
            var returnWindow = new TicketReturnWindow(purchases);
            returnWindow.ShowDialog();
        }
    }
}
