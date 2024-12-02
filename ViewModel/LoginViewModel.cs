using Kursach.Model;
using Kursach.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace Kursach.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;
        private string _password;
        private readonly AuthService _authService;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(Login, CanLogin);

        }

        public string UserLogin
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(UserLogin));
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

        public ICommand LoginCommand { get; }

        private bool CanLogin() =>
            !string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(Password);

        private void UpdateCanLogin()
        {
            (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void Login()
        {
            try
            {
                if (File.Exists("user_data.json"))
                {
                    string json = File.ReadAllText("user_data.json");
                    var users = JsonSerializer.Deserialize<List<User>>(json);

                    if (users != null)
                    {
                        var user = users.Find(u => u.Login == UserLogin && u.Password == Password);
                        if (user != null)
                        {
                            _authService.IsLoggedIn = true;
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
