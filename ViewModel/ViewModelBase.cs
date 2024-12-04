using System.ComponentModel;

// Базовий клас для всіх ViewModel, що реалізує INotifyPropertyChanged
public class ViewModelBase : INotifyPropertyChanged
{
    // Подія для сповіщення про зміну властивості
    public event PropertyChangedEventHandler PropertyChanged;

    // Метод для виклику події, коли властивість змінюється
    protected void OnPropertyChanged(string propertyName)
    {
        // Якщо є підписники на подію, викликаємо її
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
