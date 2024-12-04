using Kursach.Services;
using Kursach.ViewModel;
using System.Windows;

namespace Kursach.View
{
    public partial class MainWindow : Window
    {
        // Змінна для зберігання сервісу авторизації
        private readonly AuthService _authService;

        // Конструктор головного вікна
        public MainWindow()
        {
            InitializeComponent();

            // Ініціалізація сервісу авторизації
            _authService = new AuthService();

            // Прив'язка даних до вікна (DataContext) з використанням ViewModel
            DataContext = new MainWindowVievModel(_authService);
        }

        // Метод для відкриття вікна розкладу
        private void OpenScheduleWindow(object sender, RoutedEventArgs e)
        {
            // Логіка для відкриття вікна розкладу має бути реалізована тут
        }
    }
}
