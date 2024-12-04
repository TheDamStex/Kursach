using Kursach.Services;
using Kursach.View;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;

namespace Kursach.ViewModel
{
    // ViewModel для головного вікна
    public class MainWindowVievModel : BaseViewModel
    {
        // Сервіс авторизації
        private readonly AuthService _authService;

        // Конструктор для ініціалізації сервісу авторизації та команд
        public MainWindowVievModel(AuthService authService)
        {
            _authService = authService;
            // Ініціалізація команд
            OpenScheduleWindowCommand = new RelayCommand(OpenScheduleWindow);
            OpenTicketViewWindowCommand = new RelayCommand(OpenTicketViewWindow);
            OpenPurchaseHistoryWindowCommand = new RelayCommand(OpenPurchaseHistoryWindow);
            OpenTicketReturnWindowCommand = new RelayCommand(OpenTicketReturnWindow);
            OpenLoginWindowCommand = new RelayCommand(OpenLoginWindow);
            OpenRegistrationWindowCommand = new RelayCommand(OpenRegistrationWindow);
        }

        // Властивості для команд
        public ICommand OpenScheduleWindowCommand { get; }
        public ICommand OpenTicketViewWindowCommand { get; }
        public ICommand OpenPurchaseHistoryWindowCommand { get; }
        public ICommand OpenTicketReturnWindowCommand { get; }
        public ICommand OpenLoginWindowCommand { get; }
        public ICommand OpenRegistrationWindowCommand { get; }

        // Відкриття вікна розкладу
        public void OpenScheduleWindow()
        {
            if (_authService.IsLoggedIn)
            {
                var scheduleWindow = new ScheduleWindow();
                scheduleWindow.Show();
            }
            else
            {
                MessageBox.Show("Будь ласка, авторизуйтеся для перегляду розкладу.", "Доступ обмежено", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Відкриття вікна перегляду квитків
        public void OpenTicketViewWindow()
        {
            if (_authService.IsLoggedIn)
            {
                var ticketViewWindow = new TicketViewWindow();
                ticketViewWindow.Show();
            }
            else
            {
                MessageBox.Show("Будь ласка, авторизуйтеся для перегляду доступних квитків.", "Доступ обмежено", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Відкриття вікна історії покупок
        public void OpenPurchaseHistoryWindow()
        {
            if (_authService.IsLoggedIn)
            {
                var purchaseHistoryWindow = new PurchaseHistoryWindow();
                purchaseHistoryWindow.Show();
            }
            else
            {
                MessageBox.Show("Будь ласка, авторизуйтеся для перегляду історії покупок.", "Доступ обмежено", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Відкриття вікна повернення квитків
        public void OpenTicketReturnWindow()
        {
            if (_authService.IsLoggedIn)
            {
                var ticketReturnWindow = new TicketReturnWindow();
                ticketReturnWindow.Show();
            }
            else
            {
                MessageBox.Show("Будь ласка, авторизуйтеся для повернення квитка.", "Доступ обмежено", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Відкриття вікна авторизації
        public void OpenLoginWindow()
        {
            if (_authService.IsLoggedIn == true)
            {
                MessageBox.Show("Ви вже авторизовані", "Доступ обмежено", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var loginWindow = new LoginWindow(_authService);
                loginWindow.Show();
            }
        }

        // Відкриття вікна реєстрації
        public void OpenRegistrationWindow()
        {
            var registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
    }
}
