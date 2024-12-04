using Kursach.Services;
using Kursach.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Kursach.View
{
    public partial class TicketViewWindow : Window
    {
        // Конструктор вікна перегляду квитків
        public TicketViewWindow()
        {
            InitializeComponent();

            // Прив'язка даних для вікна перегляду квитків через ViewModel (TicketViewViewModel)
            DataContext = new TicketViewViewModel(); // Перевірка, що DataContext встановлено на відповідну ViewModel
        }
    }
}
