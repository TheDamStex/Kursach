using Kursach.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Kursach.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

        // Обработчик для обновления пароля в ViewModel через DataContext
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is LoginViewModel viewModel)
            {
                // Обновляем свойство Password в ViewModel через код
                viewModel.Password = (sender as PasswordBox)?.Password;
            }
        }
    }
}
