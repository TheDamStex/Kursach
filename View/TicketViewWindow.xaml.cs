using Kursach.Services;
using Kursach.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Kursach.View
{
    public partial class TicketViewWindow : Window
    {
        private List<Purchase> purchases;

        public TicketViewWindow(List<Purchase> purchases)
        {
            InitializeComponent();
            this.purchases = purchases;
            DataContext = new TicketViewViewModel(); // Проверь, что DataContext установлен на соответствующую ViewModel
        }

    }
}
