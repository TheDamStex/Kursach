using Kursach.ViewModel;
using Kursach.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Kursach.View
{
    // Клас вікна входу
    public partial class LoginWindow : Window
    {
        // Конструктор вікна, приймає сервіс авторизації
        public LoginWindow(AuthService authService)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(authService); // Прив'язуємо модель до вікна
        }

        // Обробник зміни пароля в полі вводу пароля
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = (sender as PasswordBox)?.Password; // Оновлюємо пароль у ViewModel
            }
        }

        // Обробник події отримання фокуса на полі вводу логіна
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginHint.Visibility = Visibility.Collapsed; // Ховаємо підказку, коли поле активно
        }

        // Обробник події втрати фокуса на полі вводу логіна
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                LoginHint.Visibility = Visibility.Visible; // Показуємо підказку, якщо поле порожнє
            }
        }

        // Обробник події отримання фокуса на полі вводу пароля
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordHint.Visibility = Visibility.Collapsed; // Ховаємо підказку, коли поле активно
        }

        // Обробник події втрати фокуса на полі вводу пароля
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                PasswordHint.Visibility = Visibility.Visible; // Показуємо підказку, якщо поле порожнє
            }
        }

        // Обробник події завантаження елементу TextBlock для анімації
        private void HeaderTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            // Отримуємо об'єкт TextBlock, на якому буде застосована анімація
            var headerTextBlock = sender as TextBlock;

            if (headerTextBlock != null)
            {
                // Запускаємо анімацію зміни кольору тексту
                var colorAnimation = (Storyboard)this.Resources["TextColorAnimation"];
                colorAnimation.Begin(); // Починаємо анімацію
            }
        }
    }
}
