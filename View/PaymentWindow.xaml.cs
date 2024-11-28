using Kursach.Model;
using Kursach.ViewModel;
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
            
        }
        

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is PaymentViewModel viewModel)
            {
                viewModel.CVV = ((PasswordBox)sender).Password;
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Скрываем водяной знак, если в поле уже есть текст
                TextBlock watermark = FindChild<TextBlock>(textBox, "WatermarkTextBlock");
                if (watermark != null && string.IsNullOrEmpty(textBox.Text))
                {
                    watermark.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Показываем водяной знак, если поле пустое
                TextBlock watermark = FindChild<TextBlock>(textBox, "WatermarkTextBlock");
                if (watermark != null && string.IsNullOrEmpty(textBox.Text))
                {
                    watermark.Visibility = Visibility.Visible;
                }
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Скрываем подсказку
                TextBlock watermark = FindChild<TextBlock>(passwordBox, "WatermarkTextBlock");
                if (watermark != null)
                {
                    watermark.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Показываем подсказку, если поле пустое
                TextBlock watermark = FindChild<TextBlock>(passwordBox, "WatermarkTextBlock");
                if (watermark != null && string.IsNullOrEmpty(passwordBox.Password))
                {
                    watermark.Visibility = Visibility.Visible;
                }
            }
        }

        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    T childOfType = (T)child;
                    if (childOfType is FrameworkElement fe && fe.Name == childName)
                    {
                        return childOfType;
                    }
                }

                // Рекурсивно ищем в дочерних элементах
                T result = FindChild<T>(child, childName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

    }

}
