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
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.TouristVMs;

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for TouristNotificationView.xaml
    /// </summary>
    public partial class TouristNotificationView : UserControl
    {
        public TouristNotificationView()
        {
            InitializeComponent();
            //this.DataContext = new TouristNotificationViewModel(activeTours, loggedInUser);
        }
    }
}
