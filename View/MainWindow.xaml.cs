using System.Collections.Generic;
using System.Windows;
using Kursach.Model;

namespace Kursach.View
{
    public partial class MainWindow : Window
    {
        private List<Purchase> purchases = new List<Purchase>();
        private bool isUserLoggedIn = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenScheduleWindow(object sender, RoutedEventArgs e)
        {
            if (isUserLoggedIn)
            {
                ScheduleWindow scheduleWindow = new ScheduleWindow();
                scheduleWindow.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь для просмотра расписания.", "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OpenLoginWindow(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }

        private void OpenTicketViewWindow(object sender, RoutedEventArgs e)
        {
            if (isUserLoggedIn)
            {
                TicketViewWindow ticketViewWindow = new TicketViewWindow(purchases);
                ticketViewWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь для просмотра доступных билетов.", "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OpenPurchaseHistoryWindow(object sender, RoutedEventArgs e)
        {
            if (isUserLoggedIn)
            {
                PurchaseHistoryWindow purchaseHistoryWindow = new PurchaseHistoryWindow(purchases);
                purchaseHistoryWindow.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь для просмотра истории покупок.", "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OpenTicketReturnWindow(object sender, RoutedEventArgs e)
        {
            if (isUserLoggedIn)
            {
                TicketReturnWindow returnWindow = new TicketReturnWindow(purchases);
                returnWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь для возврата билета.", "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
