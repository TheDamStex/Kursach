namespace Kursach.Services
{
    public class AuthService : BaseViewModel
    {
        private bool _isLoggedIn;
        private string _currentUser;
        public static int CurrentUserId { get; set; } // ID текущего пользователя

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public string CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }


    }

}
