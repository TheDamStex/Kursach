using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursach.View
{
    /// <summary>
    /// Логіка взаємодії для вікна повернення квитка (TicketReturnWindow.xaml)
    /// </summary>
    public partial class TicketReturnWindow : Window
    {
        // Конструктор вікна повернення квитка
        public TicketReturnWindow()
        {
            InitializeComponent();

            // Прив'язка даних для вікна повернення квитка через ViewModel (TicketReturnViewModel)
            DataContext = new TicketReturnViewModel();
        }
    }
}
