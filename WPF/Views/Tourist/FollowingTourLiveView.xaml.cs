﻿using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
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

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for FollowingTourLiveView.xaml
    /// </summary>
    public partial class FollowingTourLiveView : Window
    {
        public FollowingTourLiveView(TourInstance activeTour)
        {
            InitializeComponent();
            this.DataContext = new FollowingTourLiveViewModel(activeTour);
        }
    }
}
