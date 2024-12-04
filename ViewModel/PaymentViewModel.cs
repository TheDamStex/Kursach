using Kursach.Validation;
using System;
using System.Linq;
using System.Windows;

namespace Kursach.ViewModel
{
    // ViewModel для обробки платежів
    public class PaymentViewModel : ViewModelBase
    {
        // Номер картки, дата закінчення терміну дії та CVV
        private string _cardNumber;
        private string _expiryDate;
        private string _cvv;

        // Команда для виконання платежу
        public RelayCommand PayCommand { get; }
        // Подія для сповіщення про успішний платіж
        public event Action PaymentSuccessful;

        // Конструктор для ініціалізації команди
        public PaymentViewModel()
        {
            PayCommand = new RelayCommand(ExecutePayCommand);
        }

        // Властивість для отримання та встановлення номера картки
        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                // Сповіщає про зміну номера картки
                OnPropertyChanged(nameof(CardNumber));
            }
        }

        // Властивість для отримання та встановлення дати закінчення терміну дії картки
        public string ExpiryDate
        {
            get => _expiryDate;
            set
            {
                _expiryDate = value;
                // Сповіщає про зміну дати закінчення терміну дії картки
                OnPropertyChanged(nameof(ExpiryDate));
            }
        }

        // Властивість для отримання та встановлення CVV-коду
        public string CVV
        {
            get => _cvv;
            set
            {
                _cvv = value;
                // Сповіщає про зміну CVV-коду
                OnPropertyChanged(nameof(CVV));
            }
        }

        // Метод для виконання платежу
        private void ExecutePayCommand()
        {
            // Перевірка на правильність номера картки
            if (!PaymentValidation.IsValidCardNumber(CardNumber))
            {
                MessageBox.Show("Некоректний номер картки. Перевірте введені дані.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка на правильність дати закінчення терміну дії картки
            if (!PaymentValidation.IsValidExpirationDate(ExpiryDate))
            {
                MessageBox.Show("Некоректна дата закінчення терміну дії картки.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка на правильність CVV-коду
            if (!PaymentValidation.IsValidCVV(CVV))
            {
                MessageBox.Show("Некоректний CVV-код.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Якщо всі дані валідні, виконуємо платіж
            MessageBox.Show("Оплата успішно виконана!", "Оплата", MessageBoxButton.OK, MessageBoxImage.Information);
            // Закриваємо вікно з поточним DataContext
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
            // Сповіщаємо про успішну оплату
            PaymentSuccessful?.Invoke();
        }
    }
}
