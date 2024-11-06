using System.Collections.Generic;
using System.Windows;

namespace Kursach
{
    public partial class PurchaseHistoryWindow : Window
    {
        private List<Purchase> purchases;

        public PurchaseHistoryWindow(List<Purchase> purchases)
        {
            InitializeComponent();
            this.purchases = purchases;
            LoadPurchaseHistory();
        }

        private void LoadPurchaseHistory()
        {
            PurchaseHistoryListView.ItemsSource = purchases;
        }
    }
}
