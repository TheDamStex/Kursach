using System.ComponentModel;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    // Подія, яка сповіщає про зміни властивостей
    public event PropertyChangedEventHandler PropertyChanged;

    // Метод для виклику події при зміні властивості
    protected virtual void OnPropertyChanged(string propertyName)
    {
        // Викликається подія PropertyChanged, якщо є підписники
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
