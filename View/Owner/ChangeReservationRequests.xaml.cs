using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for ChangeReservationRequests.xaml
    /// </summary>
    public partial class ChangeReservationRequests : Window
    {
        private readonly ChangeReservationRequestsViewModel viewModel;
        public ChangeReservationRequests(User user)
        {
            viewModel = new ChangeReservationRequestsViewModel(user.Id);
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Accept_Click();
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Decline_Click();
        }
    }
}
