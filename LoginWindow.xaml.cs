using System.IO; 
using System.Text.Json; 
using System.Windows;
using System.Windows.Media;

namespace Kursach
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
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

        // Обработчик события нажатия кнопки "Увійти"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение данных из текстового поля и поля пароля
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            // Проверка существования файла с данными пользователя
            if (File.Exists("user_data.json"))
            {
                // Чтение данных из файла
                string json = File.ReadAllText("user_data.json");
                var user = JsonSerializer.Deserialize<User>(json); // Десериализация данных

                // Проверка правильности логина и пароля
                if (user.Login == login && user.Password == password)
                {
                    MessageBox.Show("Авторизация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // Закрытие окна после успешной авторизации
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
    }
}

