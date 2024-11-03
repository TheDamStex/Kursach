using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Открытие окна с расписанием автобусов
        private void OpenScheduleWindow(object sender, RoutedEventArgs e)
        {
            ScheduleWindow scheduleWindow = new ScheduleWindow();
            scheduleWindow.Show();
        }

        // Открытие окна оформления билетов
         private void OpenTicketWindow(object sender, RoutedEventArgs e)
         {
         TicketWindow ticketWindow = new TicketWindow();
         ticketWindow.Show();
         }

        // Открытие окна авторизации
        private void OpenLoginWindow(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        // Открытие окна регистрации
        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
    }
}

