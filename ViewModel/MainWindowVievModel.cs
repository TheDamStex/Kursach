using Kursach.Services;
using Kursach.View;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;

namespace Kursach.ViewModel
{
    public class MainWindowVievModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private List<Purchase> _purchases;

        public MainWindowVievModel(AuthService authService)
        {
            _authService = authService;
            _purchases = new List<Purchase>();

            // Инициализация команд
            OpenScheduleWindowCommand = new RelayCommand(OpenScheduleWindow);
            OpenTicketViewWindowCommand = new RelayCommand(OpenTicketViewWindow);
            OpenPurchaseHistoryWindowCommand = new RelayCommand(OpenPurchaseHistoryWindow);
            OpenTicketReturnWindowCommand = new RelayCommand(OpenTicketReturnWindow);
            OpenLoginWindowCommand = new RelayCommand(OpenLoginWindow);
            OpenRegistrationWindowCommand = new RelayCommand(OpenRegistrationWindow);
        }

        // Свойства для команд
        public ICommand OpenScheduleWindowCommand { get; }
        public ICommand OpenTicketViewWindowCommand { get; }
        public ICommand OpenPurchaseHistoryWindowCommand { get; }
        public ICommand OpenTicketReturnWindowCommand { get; }
        public ICommand OpenLoginWindowCommand { get; }
        public ICommand OpenRegistrationWindowCommand { get; }

        public void OpenScheduleWindow()
        {
            if (_authService.IsLoggedIn)
            {
                var scheduleWindow = new ScheduleWindow();
                scheduleWindow.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь для просмотра расписания.", "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void OpenTicketViewWindow()
        {
            if (_authService.IsLoggedIn)
            {
                var ticketViewWindow = new TicketViewWindow(_purchases);
                ticketViewWindow.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь для просмотра доступных билетов.", "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void OpenPurchaseHistoryWindow()
        {
            if (_authService.IsLoggedIn)
            {
                var purchaseHistoryWindow = new PurchaseHistoryWindow(_purchases);
                purchaseHistoryWindow.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь для просмотра истории покупок.", "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void OpenTicketReturnWindow()
        {
            if (_authService.IsLoggedIn)
            {
                var ticketReturnWindow = new TicketReturnWindow(_purchases);
                ticketReturnWindow.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь для возврата билета.", "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void OpenLoginWindow()
        {
            var loginWindow = new LoginWindow(_authService);
            loginWindow.Show();
        }

        public void OpenRegistrationWindow()
        {
            var registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
    }
}
