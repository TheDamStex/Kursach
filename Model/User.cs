namespace Kursach.Model
{
    // Клас, що описує користувача системи
    public class User
    {
        public int UserId { get; set; } // Унікальний ідентифікатор користувача (ціле число)
        public string Login { get; set; } // Логін користувача
        public string Password { get; set; } // Пароль користувача
    }
}
