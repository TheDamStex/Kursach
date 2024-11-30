using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kursach.View
{
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
            InitializeWatermarkVisibility();
            this.Deactivated += PaymentWindow_Deactivated; // Обработка события потери фокуса окна
        }

        private void InitializeWatermarkVisibility()
        {
            // Для каждого TextBox и PasswordBox проверяем, есть ли текст. Если нет, показываем водяной знак
            SetWatermarkVisibility(FullName);
            SetWatermarkVisibility(CardNumber);
            SetWatermarkVisibility(ExpiryDate);
            SetWatermarkVisibility(PhoneNumber);
            SetPasswordWatermarkVisibility(CVV);
        }

        private void SetWatermarkVisibility(TextBox textBox)
        {
            var watermark = (TextBlock)textBox.Template.FindName("WatermarkTextBlock", textBox);
            if (watermark != null)
            {
                watermark.Visibility = string.IsNullOrWhiteSpace(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void SetPasswordWatermarkVisibility(PasswordBox passwordBox)
        {
            var watermark = (TextBlock)passwordBox.Template.FindName("WatermarkTextBlock", passwordBox);
            if (watermark != null)
            {
                watermark.Visibility = string.IsNullOrWhiteSpace(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void PaymentWindow_Deactivated(object sender, EventArgs e)
        {
            // Когда окно теряет фокус, проверяем все текстовые поля и PasswordBox
            SetWatermarkVisibility(FullName);
            SetWatermarkVisibility(CardNumber);
            SetWatermarkVisibility(ExpiryDate);
            SetWatermarkVisibility(PhoneNumber);
            SetPasswordWatermarkVisibility(CVV);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var watermark = (TextBlock)textBox.Template.FindName("WatermarkTextBlock", textBox);
                if (watermark != null)
                {
                    watermark.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Проверка, есть ли текст. Если нет, водяной знак показывается.
                var watermark = (TextBlock)textBox.Template.FindName("WatermarkTextBlock", textBox);
                if (watermark != null)
                {
                    watermark.Visibility = string.IsNullOrWhiteSpace(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                var watermark = (TextBlock)passwordBox.Template.FindName("WatermarkTextBlock", passwordBox);
                if (watermark != null)
                {
                    watermark.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                var watermark = (TextBlock)passwordBox.Template.FindName("WatermarkTextBlock", passwordBox);
                if (watermark != null)
                {
                    watermark.Visibility = string.IsNullOrWhiteSpace(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Обновление видимости водяного знака при изменении текста
                var watermark = (TextBlock)textBox.Template.FindName("WatermarkTextBlock", textBox);
                if (watermark != null)
                {
                    watermark.Visibility = string.IsNullOrWhiteSpace(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                // Обновление видимости водяного знака при изменении пароля
                var watermark = (TextBlock)passwordBox.Template.FindName("WatermarkTextBlock", passwordBox);
                if (watermark != null)
                {
                    watermark.Visibility = string.IsNullOrWhiteSpace(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
