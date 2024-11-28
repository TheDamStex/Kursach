using Kursach.Services;
using Kursach.ViewModel;
using System.Windows;

namespace Kursach.View
{
    public partial class MainWindow : Window
    {
        private readonly AuthService _authService;

        public MainWindow()
        {
            InitializeComponent();
            _authService = new AuthService();
            DataContext = new MainWindowVievModel(_authService);

        }

        private void OpenScheduleWindow(object sender, RoutedEventArgs e)
        {

        }
    }
}
