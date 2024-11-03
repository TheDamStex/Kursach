using System.Windows;
using System;
using System.IO;
using System.Text.Json;

namespace Kursach
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение данных из текстовых полей
            string login = LoginTextBox.Text;
            string password = PasswordBox1.Password;
            string confirmPassword = PasswordBox2.Password;

            // Проверка совпадения паролей
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Создание объекта пользователя
            var user = new User
            {
                Login = login,
                Password = password
            };

            // Сериализация в JSON
            string json = JsonSerializer.Serialize(user);

            // Сохранение JSON в файл
            string filePath = "user_data.json";
            File.WriteAllText(filePath, json);

            MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    // Модель пользователя
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
