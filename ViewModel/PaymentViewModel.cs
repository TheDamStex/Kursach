using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Kursach.ViewModel
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private string _cardNumber;
        private string _cardHolderName;
        private string _expiryDate;
        private string _cvv;

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                OnPropertyChanged();
                PayCommand.RaiseCanExecuteChanged(); // Обновляем CanExecute при изменении данных
            }
        }

        public string CardHolderName
        {
            get => _cardHolderName;
            set
            {
                _cardHolderName = value;
                OnPropertyChanged();
                PayCommand.RaiseCanExecuteChanged();
            }
        }

        public string ExpiryDate
        {
            get => _expiryDate;
            set
            {
                _expiryDate = value;
                OnPropertyChanged();
                PayCommand.RaiseCanExecuteChanged();
            }
        }

        public string CVV
        {
            get => _cvv;
            set
            {
                _cvv = value;
                OnPropertyChanged();
                PayCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand PayCommand { get; }

        public PaymentViewModel()
        {
            PayCommand = new RelayCommand(ExecutePay, CanExecutePay);
        }

        private bool CanExecutePay()
        {
            // Проверка на заполненность всех полей
            return !string.IsNullOrWhiteSpace(CardNumber) &&
                   !string.IsNullOrWhiteSpace(CardHolderName) &&
                   !string.IsNullOrWhiteSpace(ExpiryDate) &&
                   !string.IsNullOrWhiteSpace(CVV);
        }

        private void ExecutePay()
        {
            // Логика оплаты
            if (ValidateCardDetails())
            {
                MessageBox.Show("Оплата успешно выполнена!", "Успех");
            }
            else
            {
                MessageBox.Show("Ошибка в данных карты. Проверьте ввод!", "Ошибка");
            }
        }

        private bool ValidateCardDetails()
        {
            // Проверка корректности номера карты
            if (CardNumber.Length != 16 || !long.TryParse(CardNumber, out _))
                return false;

            // Проверка срока действия (формат MM/YY)
            if (!DateTime.TryParseExact("01/" + ExpiryDate, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expiryDate))
                return false;

            if (expiryDate < DateTime.Now)
                return false;

            // Проверка CVV
            if (CVV.Length != 3 || !int.TryParse(CVV, out _))
                return false;

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
