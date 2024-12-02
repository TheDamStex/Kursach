using Kursach.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kursach.View
{
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
            DataContext = new PaymentViewModel();
        }
        private void CVV_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            var viewModel = DataContext as PaymentViewModel;
            if (viewModel != null)
            {
                viewModel.CVV = passwordBox.Password;
            }
        }

    }
}
