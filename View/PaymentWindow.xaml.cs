using Kursach.Model;
using Kursach.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kursach.View
{
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
            DataContext = new PaymentViewModel();
        }
        

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is PaymentViewModel viewModel)
            {
                viewModel.CVV = ((PasswordBox)sender).Password;
            }
        }
    }
}
