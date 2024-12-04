namespace Kursach.Services
{
    // Сервіс для авторизації
    public class AuthService : BaseViewModel
    {
        // Прапорець для перевірки, чи користувач авторизований
        private bool _isLoggedIn;
        // Поточний користувач
        private int _currentUser;

        // Статична властивість для зберігання ID поточного користувача
        public static int CurrentUserId { get; set; } // ID поточного користувача

        // Властивість для перевірки стану авторизації
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                // Викликається при зміні стану авторизації
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        // Властивість для зберігання ID поточного користувача
        public int CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                // Викликається при зміні ID користувача
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
    }
}
