using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Kursach.ViewModel;

namespace Kursach.View
{
    public partial class RegistrationWindow : Window
    {
        // Конструктор вікна реєстрації
        public RegistrationWindow()
        {
            InitializeComponent();

            // Прив'язка даних до вікна (DataContext) з використанням ViewModel для реєстрації
            DataContext = new RegistrationViewModel();
        }

        // Обробка зміни пароля в першому полі
        private void PasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel)
            {
                // Оновлюємо пароль в ViewModel
                viewModel.Password = PasswordBox1.Password;
            }
        }

        // Обробка зміни пароля в другому полі (для підтвердження)
        private void PasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel)
            {
                // Оновлюємо підтвердження пароля в ViewModel
                viewModel.ConfirmPassword = PasswordBox2.Password;
            }
        }

        // Обробка фокусу на полі вводу логіну
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // При фокусі на полі ховаємо підказку
            LoginHint.Visibility = Visibility.Collapsed;
        }

        // Обробка втрати фокусу на полі вводу логіну
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Якщо поле порожнє, показуємо підказку
            if (string.IsNullOrEmpty((sender as TextBox)?.Text))
            {
                LoginHint.Visibility = Visibility.Visible;
            }
        }

        // Обробка фокусу на полях пароля
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // При фокусі на полі ховаємо підказки для пароля
            PasswordHint1.Visibility = Visibility.Collapsed;
            PasswordHint2.Visibility = Visibility.Collapsed;
        }

        // Обробка втрати фокусу на полях пароля
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Якщо поле пароля порожнє, показуємо підказку
            if (string.IsNullOrEmpty(PasswordBox1.Password))
            {
                PasswordHint1.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(PasswordBox2.Password))
            {
                PasswordHint2.Visibility = Visibility.Visible;
            }
        }

        // Анімація для заголовку
        private void HeaderTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            // Отримуємо анімацію кольору для заголовка
            var colorAnimation = (ColorAnimation)FindResource("ColorAnimation");
            var colorStoryboard = new Storyboard();
            colorStoryboard.Children.Add(colorAnimation);

            // Прив'язуємо анімацію до кольору тексту в заголовку
            Storyboard.SetTarget(colorAnimation, HeaderTextBlock);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(TextBlock.Foreground).(SolidColorBrush.Color)"));

            // Запускаємо анімацію
            colorStoryboard.Begin();
        }
    }
}
