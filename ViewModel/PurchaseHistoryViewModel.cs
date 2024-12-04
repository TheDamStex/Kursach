using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Kursach.Services;
using Kursach.Model;
using System.Windows;

public class PurchaseHistoryViewModel : ViewModelBase
{
    // Колекція для збереження квитків користувача
    public ObservableCollection<UserTicket> UserTickets { get; private set; }

    // Конструктор для ініціалізації колекції та завантаження історії покупок
    public PurchaseHistoryViewModel()
    {
        UserTickets = new ObservableCollection<UserTicket>();
        LoadUserTickets();
    }

    // Метод для завантаження квитків користувача
    private void LoadUserTickets()
    {
        const string userTicketsFilePath = "user_tickets.json";

        // Перевірка, чи існує файл з квитками
        if (File.Exists(userTicketsFilePath))
        {
            try
            {
                // Читання всіх квитків з файлу
                var allTickets = JsonSerializer.Deserialize<List<UserTicket>>(File.ReadAllText(userTicketsFilePath));

                if (allTickets != null)
                {
                    // Фільтрація квитків для поточного користувача
                    var currentUserTickets = allTickets
                        .Where(t => t.UserId == AuthService.CurrentUserId)
                        .ToList();

                    // Заповнення колекції квитками поточного користувача
                    foreach (var ticket in currentUserTickets)
                    {
                        UserTickets.Add(ticket);
                    }
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок при завантаженні історії
                MessageBox.Show($"Помилка при завантаженні історії квитків: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            // Повідомлення про відсутність файлу з квитками
            MessageBox.Show("Файл з квитками не знайдений.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
