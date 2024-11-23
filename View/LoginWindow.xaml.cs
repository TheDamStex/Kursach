using Kursach.ViewModel;
using Kursach.Services;
using System.Windows;
using System.Windows.Controls;

namespace Kursach.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(AuthService authService)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(authService);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = (sender as PasswordBox)?.Password;
            }
        }
    }
}
