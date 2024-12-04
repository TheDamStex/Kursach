using System.Windows.Input;
using System;

public class RelayCommand : ICommand
{
    // Дії, які виконуються при виконанні команди
    private readonly Action execute;

    // Функція, яка перевіряє, чи можна виконати команду
    private readonly Func<bool> canExecute;

    // Конструктор команди
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute)); // Перевірка, що виконання передано
        this.canExecute = canExecute; // Функція перевірки виконання
    }

    // Метод, який перевіряє, чи можна виконати команду
    public bool CanExecute(object parameter)
    {
        return canExecute == null || canExecute(); // Якщо функція не надана, команда завжди виконується
    }

    // Метод виконання команди
    public void Execute(object parameter)
    {
        execute(); // Виконується передана дія
    }

    // Подія для оновлення можливості виконання команди
    public event EventHandler CanExecuteChanged;

    // Метод для підняття події зміни можливості виконання
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty); // Оновлює статус виконання команди
    }
}
