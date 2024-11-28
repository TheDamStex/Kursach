using Kursach.Model;
using Kursach.View;
using System;
using System.Linq;
using System.Windows;

namespace Kursach.ViewModel
{
    public class PaymentViewModel : ViewModelBase
    {
        private Flight _selectedFlight;

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

        public RelayCommand PayCommand { get; }

        public PaymentViewModel(Flight selectedFlight)
        {
            _selectedFlight = selectedFlight;
            PayCommand = new RelayCommand(ExecutePay, CanExecutePay);
        }

        private bool CanExecutePay()
        {
            return !string.IsNullOrWhiteSpace(CardNumber) &&
                   !string.IsNullOrWhiteSpace(CardHolderName) &&
                   !string.IsNullOrWhiteSpace(ExpiryDate) &&
                   !string.IsNullOrWhiteSpace(CVV);
        }

        private void ExecutePay()
        {
            if (ValidateCardDetails())
            {
                if (_selectedFlight.FreeSeats > 0)
                {
                    _selectedFlight.FreeSeats--;
                    MessageBox.Show("Оплата прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<PaymentWindow>().FirstOrDefault()?.Close();
                }
                else
                {
                    MessageBox.Show("Нет свободных мест!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Некорректные данные карты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool ValidateCardDetails()
        {
            // Простейшая проверка данных карты
            return CardNumber.Length == 16 && CVV.Length == 3 && DateTime.TryParseExact("01/" + ExpiryDate, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expiryDate) && expiryDate > DateTime.Now;
        }


    }
}
