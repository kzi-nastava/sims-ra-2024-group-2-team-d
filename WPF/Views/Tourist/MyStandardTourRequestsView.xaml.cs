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
using BookingApp.WPF.ViewModels;

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for MyStandardTourRequestsView.xaml
    /// </summary>
    public partial class MyStandardTourRequestsView : Window
    {
        public MyStandardTourRequestsView(User loggedInUser)
        {
            InitializeComponent();
            DataContext = new MyStandardTourRequestsViewModel(loggedInUser);
        }
    }
}
