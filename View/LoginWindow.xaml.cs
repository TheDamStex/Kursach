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
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginHint.Visibility = Visibility.Collapsed; // Скрыть подсказку, когда поле активно
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                LoginHint.Visibility = Visibility.Visible; // Показать подсказку, если поле пустое
            }
        }

        // Подсказки для пароля
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordHint.Visibility = Visibility.Collapsed; // Скрыть подсказку, когда поле активно
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                PasswordHint.Visibility = Visibility.Visible; // Показать подсказку, если поле пустое
            }
        }
    }
}
