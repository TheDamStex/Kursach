using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Kursach.Model;

namespace Kursach.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;
        private string _password;
        private bool _isLoggedIn;

        // Свойства
        public string UserLogin // Переименовал свойство, чтобы избежать конфликта
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(UserLogin)); // Исправил на новое имя свойства
                UpdateCanLogin();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                UpdateCanLogin();
            }
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            private set
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        // Команда для логина
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            // Инициализация команды с указанием метода и условия выполнения
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        // Метод для проверки, можно ли выполнить логин
        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(Password);
        }

        // Обновляем состояние команды логина
        private void UpdateCanLogin()
        {
            (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        // Метод для выполнения логина
        private void Login()
        {
            try
            {
                if (File.Exists("user_data.json"))
                {
                    // Чтение данных пользователя
                    string json = File.ReadAllText("user_data.json");
                    var user = JsonSerializer.Deserialize<User>(json);

                    if (user == null)
                    {
                        MessageBox.Show("Ошибка данных пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Проверка логина и пароля
                    if (user.Login == this.UserLogin && user.Password == this.Password) // Используем переименованное свойство
                    {
                        IsLoggedIn = true;
                        MessageBox.Show("Авторизация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь не зарегистрирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
