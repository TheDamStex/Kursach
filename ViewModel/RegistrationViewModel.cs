using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Kursach.Model;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Kursach.ViewModel
{
    // ViewModel для реєстрації користувача
    public class RegistrationViewModel : BaseViewModel
    {
        private string login;
        private string password;
        private string confirmPassword;

        // Властивість для логіна користувача
        public string Login
        {
            get => login;
            set
            {
                login = value;
                // Сповіщає про зміну логіна
                OnPropertyChanged(nameof(Login));
                UpdateCanRegister();
            }
        }

        // Властивість для пароля користувача
        public string Password
        {
            get => password;
            set
            {
                password = value;
                // Сповіщає про зміну пароля
                OnPropertyChanged(nameof(Password));
                UpdateCanRegister();
            }
        }

        // Властивість для підтвердження пароля
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                // Сповіщає про зміну підтвердження пароля
                OnPropertyChanged(nameof(ConfirmPassword));
                UpdateCanRegister();
            }
        }

        // Властивість для визначення можливості реєстрації
        private bool _canRegister;
        public bool CanRegister
        {
            get => _canRegister;
            set
            {
                _canRegister = value;
                // Сповіщає про зміну стану можливості реєстрації
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        // Команда для реєстрації користувача
        public ICommand RegisterCommand { get; }

        // Конструктор для ініціалізації команди реєстрації
        public RegistrationViewModel()
        {
            RegisterCommand = new RelayCommand(Register, () => CanRegister);
        }

        // Оновлення можливості реєстрації на основі введених даних
        private void UpdateCanRegister()
        {
            Console.WriteLine($"Login: '{Login}', Password: '{Password}', ConfirmPassword: '{ConfirmPassword}'");
            CanRegister = !string.IsNullOrWhiteSpace(Login) &&
                          !string.IsNullOrWhiteSpace(Password) &&
                          Password == ConfirmPassword;
            Console.WriteLine($"CanRegister updated: {CanRegister}");
            // Оновлення стану команди
            (RegisterCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        // Метод для реєстрації користувача
        private void Register()
        {
            // Читання існуючих даних користувачів
            List<User> users = new List<User>();
            if (File.Exists("user_data.json"))
            {
                string existingData = File.ReadAllText("user_data.json");
                if (!string.IsNullOrWhiteSpace(existingData))
                {
                    users = JsonSerializer.Deserialize<List<User>>(existingData);
                }
            }

            // Перевірка, чи існує вже користувач з таким логіном
            if (users.Exists(u => u.Login == Login))
            {
                MessageBox.Show("Користувач з таким логіном вже існує!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Генерація нового унікального ID
            int newUserId = users.Count > 0 ? users.Max(u => u.UserId) + 1 : 1;

            // Додавання нового користувача
            var newUser = new User
            {
                UserId = newUserId, // Присвоюємо унікальний ID
                Login = Login,
                Password = Password
            };
            users.Add(newUser);

            // Сериалізація та запис в файл
            string updatedJson = JsonSerializer.Serialize(users);
            File.WriteAllText("user_data.json", updatedJson);

            // Очищення полів після успішної реєстрації
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;

            // Виведення повідомлення про успішну реєстрацію
            MessageBox.Show("Реєстрація успішна!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
