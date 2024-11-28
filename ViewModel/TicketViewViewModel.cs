using Kursach.View;
using Kursach.ViewModel;
using Kursach.Model;
using Kursach;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

public class TicketViewViewModel : ViewModelBase
{
    public ICommand OpenPaymentWindowCommand { get; }
    public ObservableCollection<Flight> Flights { get; set; }

    private Flight _selectedFlight;
    public Flight SelectedFlight
    {
        get => _selectedFlight;
        set
        {
            _selectedFlight = value;
            OnPropertyChanged(nameof(SelectedFlight));
            ((RelayCommand)OpenPaymentWindowCommand).RaiseCanExecuteChanged();
        }
    }


    public TicketViewViewModel()
    {
        OpenPaymentWindowCommand = new RelayCommand(ExecuteOpenPaymentWindowCommand, CanExecuteOpenPaymentWindowCommand);
        
        Flights = new ObservableCollection<Flight>
        {

                new Flight { FlightNumber = "101", InitialPoint = "Кременчук", FinalDestination = "Київ", IntermediateStops = "Полтава", DepartureTime = "08:30", FreeSeats = 15, Price = 150.00m },
                new Flight { FlightNumber = "102", InitialPoint = "Кременчук", FinalDestination = "Олександрія", IntermediateStops = "Полтава", DepartureTime = "09:00", FreeSeats = 10, Price = 120.00m },
                new Flight { FlightNumber = "103", InitialPoint = "Кременчук", FinalDestination = "Кропивницький", IntermediateStops = "Олександрія", DepartureTime = "09:30", FreeSeats = 5, Price = 130.00m },
                new Flight { FlightNumber = "104", InitialPoint = "Кременчук", FinalDestination = "Полтава", IntermediateStops = "Київ", DepartureTime = "10:00", FreeSeats = 20, Price = 110.00m },
        // другие рейсы...
        };
    }

    private bool CanExecuteOpenPaymentWindowCommand()
    {
        return SelectedFlight != null; // Кнопка активна только при выборе рейса
    }

    private void ExecuteOpenPaymentWindowCommand()
    {
        if (SelectedFlight != null)
        {
            var paymentWindow = new PaymentWindow
            {
                DataContext = new PaymentViewModel(SelectedFlight)
            };
            paymentWindow.Show();
        }
        else
        {
            MessageBox.Show("Пожалуйста, выберите рейс для покупки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }


}
