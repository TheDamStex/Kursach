using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;

namespace Kursach
{
    public partial class LoginWindow : Window
    {
        // Свойство для хранения состояния авторизации
        public bool IsLoggedIn { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == "Логін")
            {
                LoginTextBox.Text = "";
                LoginTextBox.Foreground = Brushes.Black;
            }
        }

        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                LoginTextBox.Text = "Логін";
                LoginTextBox.Foreground = Brushes.Gray;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (File.Exists("user_data.json"))
            {
                string json = File.ReadAllText("user_data.json");
                var user = JsonSerializer.Deserialize<User>(json);

                if (user.Login == login && user.Password == password)
                {
                    IsLoggedIn = true;  // Устанавливаем флаг авторизации в true
                    MessageBox.Show("Авторизация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
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
