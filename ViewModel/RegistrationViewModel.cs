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
            Console.WriteLine($"Login: '{Login}', Password: '{Password}', ConfirmPassword: '{ConfirmPassword}'");
            CanRegister = !string.IsNullOrWhiteSpace(Login) &&
                          !string.IsNullOrWhiteSpace(Password) &&
                          Password == ConfirmPassword;
            Console.WriteLine($"CanRegister updated: {CanRegister}");
            (RegisterCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }



        private void Register()
        {
            // Чтение существующих данных пользователей
            List<User> users = new List<User>();
            if (File.Exists("user_data.json"))
            {
                string existingData = File.ReadAllText("user_data.json");
                if (!string.IsNullOrWhiteSpace(existingData))
                {
                    users = JsonSerializer.Deserialize<List<User>>(existingData);
                }
            }

            // Проверяем, существует ли пользователь с таким логином
            if (users.Exists(u => u.Login == Login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Генерация нового уникального ID
            int newUserId = users.Count > 0 ? users.Max(u => u.UserId) + 1 : 1;

            // Добавление нового пользователя
            var newUser = new User
            {
                UserId = newUserId, // Присваиваем уникальный ID
                Login = Login,
                Password = Password
            };
            users.Add(newUser);

            // Сериализация и запись обратно в файл
            string updatedJson = JsonSerializer.Serialize(users);
            File.WriteAllText("user_data.json", updatedJson);

            // Очищаем поля после успешной регистрации
            Login = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;

            // Выводим сообщение об успешной регистрации
            MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
