using Kursach.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace Kursach.View
{
    public partial class PurchaseHistoryWindow : Window
    {
        // Конструктор вікна історії покупок
        public PurchaseHistoryWindow()
        {
            InitializeComponent();

            // Прив'язка даних до вікна (DataContext) з використанням ViewModel
            this.DataContext = new PurchaseHistoryViewModel();
        }
    }
}
