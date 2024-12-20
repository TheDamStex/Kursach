﻿using System;
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
    /// Логіка взаємодії для вікна розкладу (ScheduleWindow.xaml)
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        // Конструктор вікна розкладу
        public ScheduleWindow()
        {
            InitializeComponent();

            // Прив'язка даних для вікна розкладу через ViewModel (TicketViewViewModel)
            DataContext = new TicketViewViewModel();
        }
    }
}
