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
using BookingApp.WPF.ViewModels;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for CreateTourRequestView.xaml
    /// </summary>
    public partial class CreateTourRequestView : Window
    {
        public CreateTourRequestView(User loggedInUser)
        {
            InitializeComponent();
            DataContext = new CreateTourRequestViewModel(loggedInUser, Close);
        }
    }
}
