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
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel();
        }

        private void PasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel)
            {
                viewModel.Password = PasswordBox1.Password;
            }
        }

        private void PasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel)
            {
                viewModel.ConfirmPassword = PasswordBox2.Password;
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginHint.Visibility = Visibility.Collapsed; // Скрыть подсказку, когда поле активно
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as TextBox)?.Text))
            {
                LoginHint.Visibility = Visibility.Visible; // Показать подсказку, если поле пустое
            }
        }

        // Подсказки для пароля
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordHint1.Visibility = Visibility.Collapsed; // Скрыть подсказку, когда поле активно
            PasswordHint2.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox1.Password))
            {
                PasswordHint1.Visibility = Visibility.Visible; // Показать подсказку, если поле пустое
            }

            if (string.IsNullOrEmpty(PasswordBox2.Password))
            {
                PasswordHint2.Visibility = Visibility.Visible;
            }
        }
        private void HeaderTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            // Получаем объект TextBlock, на котором будет применена анимация
            var colorAnimation = (ColorAnimation)FindResource("ColorAnimation");
            var colorStoryboard = new Storyboard();
            colorStoryboard.Children.Add(colorAnimation);

            // Применяем анимацию
            Storyboard.SetTarget(colorAnimation, HeaderTextBlock);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(TextBlock.Foreground).(SolidColorBrush.Color)"));

            colorStoryboard.Begin();
        }
    }
}
