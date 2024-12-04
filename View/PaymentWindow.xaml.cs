using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kursach.Validation;
using Kursach.ViewModel;

namespace Kursach.View
{
    public partial class PaymentWindow : Window
    {
        // Конструктор вікна оплати
        public PaymentWindow()
        {
            InitializeComponent();

            // Прив'язка даних до вікна (DataContext) з використанням ViewModel
            DataContext = new PaymentViewModel();
        }

        // Обробка зміни пароля CVV
        private void CVV_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is PaymentViewModel viewModel)
            {
                // Оновлюємо CVV у ViewModel
                viewModel.CVV = (sender as PasswordBox)?.Password;
            }
        }

        // Обробка введення тексту в полі номера картки
        private void CardNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var currentText = textBox.Text;

            // Перевірка валідності введеного тексту для номера картки
            if (!PaymentValidation.IsCardNumberInputValid(currentText, e.Text))
            {
                e.Handled = true; // Блокуємо введення, якщо текст недопустимий
                return;
            }

            // Форматуємо введений номер картки
            string formattedText = PaymentValidation.FormatCardNumber(currentText + e.Text);
            textBox.Text = formattedText;
            textBox.SelectionStart = textBox.Text.Length;
            e.Handled = true;
        }

        // Обробка введення тексту в полі терміну дії картки
        private void ExpirationDateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var currentText = textBox.Text;

            // Перевірка валідності введеного тексту для дати закінчення терміну
            if (!PaymentValidation.IsExpirationDateInputValid(currentText, e.Text))
            {
                e.Handled = true; // Блокуємо введення, якщо текст недопустимий
                return;
            }
        }

        // Обробка введення тексту в полі CVV
        private void CVVPasswordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                var currentText = passwordBox.Password;

                // Перевірка валідності введеного тексту для CVV
                if (!PaymentValidation.IsCVVInputValid(currentText, e.Text))
                {
                    e.Handled = true; // Блокуємо введення, якщо текст недопустимий
                }
            }
        }
    }
}
