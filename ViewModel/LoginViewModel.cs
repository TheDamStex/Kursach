using Kursach.Model;
using Kursach.ViewModel;
using Kursach.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace Kursach.ViewModel
{
    // ViewModel для авторизації користувача
    public class LoginViewModel : BaseViewModel
    {
        // Логін і пароль користувача
        private string _login;
        private string _password;
        // Сервіс авторизації
        private readonly AuthService _authService;

        // Конструктор для ініціалізації сервісу авторизації та команди
        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            // Ініціалізація команди для входу
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        // Властивість для отримання та встановлення логіну користувача
        public string UserLogin
        {
            get => _login;
            set
            {
                _login = value;
                // Сповіщає про зміну логіну
                OnPropertyChanged(nameof(UserLogin));
                // Оновлює можливість виконання команди
                UpdateCanLogin();
            }
        }

        // Властивість для отримання та встановлення пароля користувача
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                // Сповіщає про зміну пароля
                OnPropertyChanged(nameof(Password));
                // Оновлює можливість виконання команди
                UpdateCanLogin();
            }
        }

        // Команда для виконання авторизації
        public ICommand LoginCommand { get; }

        // Перевірка, чи можна виконати команду (логін та пароль не повинні бути порожніми)
        private bool CanLogin() =>
            !string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(Password);

        // Оновлення можливості виконання команди
        private void UpdateCanLogin()
        {
            (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        // Метод для виконання авторизації
        private void Login()
        {
            try
            {
                // Перевірка, чи існує файл з даними користувачів
                if (File.Exists("user_data.json"))
                {
                    // Читання вмісту файлу
                    string json = File.ReadAllText("user_data.json");
                    // Десеріалізація списку користувачів
                    var users = JsonSerializer.Deserialize<List<User>>(json);

                    if (users != null)
                    {
                        // Пошук користувача за логіном та паролем
                        var user = users.Find(u => u.Login == UserLogin && u.Password == Password);
                        if (user != null)
                        {
                            // Встановлення поточного користувача
                            AuthService.CurrentUserId = user.UserId;
                            _authService.IsLoggedIn = true;

                            // Повідомлення про успішну авторизацію
                            MessageBox.Show("Авторизація успішна!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);


                        }
                        else
                        {
                            // Повідомлення про неправильний логін або пароль
                            MessageBox.Show("Невірний логін або пароль!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        // Повідомлення про відсутність користувача в списку
                        MessageBox.Show("Користувач не зареєстрований!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Повідомлення про відсутність файлу з користувачами
                    MessageBox.Show("Користувач не зареєстрований!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Повідомлення про помилку під час виконання авторизації
                MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
