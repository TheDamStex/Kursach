using Kursach.Model;
using System.Windows;
using System.Windows.Input;

namespace Kursach.ViewModel
{
    public class PaymentViewModel : ViewModelBase
    {
        private string _cardNumber;
        private string _expiryDate;
        private string _cvv;

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }
        }

        public string ExpiryDate
        {
            get => _expiryDate;
            set
            {
                _expiryDate = value;
                OnPropertyChanged(nameof(ExpiryDate));
            }
        }

        public string CVV
        {
            get => _cvv;
            set
            {
                _cvv = value;
                OnPropertyChanged(nameof(CVV));
            }
        }

        public ICommand PayCommand { get; }

        public PaymentViewModel()
        {
            PayCommand = new RelayCommand(Payment);
        }


        private void Payment()
        {
            // Логика для выполнения оплаты
            MessageBox.Show("Оплата успешно выполнена!", "Оплата", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
