using System.Windows;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace Kursach
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна
        }

        // Обработчик события получения фокуса для поля логина
        private void LoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Проверка, если текст равен подсказке
            if (LoginTextBox.Text == "Логін")
            {
                LoginTextBox.Text = ""; // Очищаем текстовое поле
                LoginTextBox.Foreground = Brushes.Black; // Меняем цвет текста на черный
            }
        }

        // Обработчик события потери фокуса для поля логина
        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Проверка, если поле пустое
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                LoginTextBox.Text = "Логін"; // Устанавливаем текст подсказки
                LoginTextBox.Foreground = Brushes.Gray; // Меняем цвет текста на серый
            }
        }

        // Обработчик события нажатия кнопки регистрации
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
                return; // Завершаем выполнение, если пароли не совпадают
            }

            // Создание объекта пользователя
            var user = new User
            {
                Login = login,
                Password = password
            };

            // Сериализация объекта пользователя в JSON
            string json = JsonSerializer.Serialize(user);
            // Указание пути для сохранения JSON файла
            string filePath = "user_data.json";
            File.WriteAllText(filePath, json); 

            // Сообщение об успешной регистрации
            MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); // Закрытие окна после успешной регистрации
        }
    }

    // Модель пользователя
    public class User
    {
        public string Login { get; set; } // Логин пользователя
        public string Password { get; set; } // Пароль пользователя
    }
}
