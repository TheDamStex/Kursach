using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Kursach.Model;


namespace Kursach.ViewModel
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string login;
        private string password;
        private string confirmPassword;

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
                UpdateCanRegister();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
                UpdateCanRegister();
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                UpdateCanRegister();
            }
        }

        private bool _canRegister;
        public bool CanRegister
        {
            get => _canRegister;
            set
            {
                _canRegister = value;
                OnPropertyChanged(nameof(CanRegister)); // Уведомляем о смене значения
            }
        }


        public ICommand RegisterCommand { get; }

        public RegistrationViewModel()
        {
            RegisterCommand = new RelayCommand(Register, () => CanRegister);
        }

        private void UpdateCanRegister()
        {
            CanRegister = !string.IsNullOrWhiteSpace(Login) &&
                          !string.IsNullOrWhiteSpace(Password) &&
                          Password == ConfirmPassword;
            (RegisterCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void Register()
        {
            // Сохранение данных пользователя
            var user = new User { Login = Login, Password = Password };
            string json = JsonSerializer.Serialize(user);
            File.WriteAllText("user_data.json", json);

            MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
