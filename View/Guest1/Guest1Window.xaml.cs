using BookingApp.Model;
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

namespace BookingApp.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1Window.xaml
    /// </summary>
    public partial class Guest1Window : Window
    {
        private readonly User _user;
        public Guest1Window(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void AccommodationDisplay_Click(object sender, RoutedEventArgs e)
        {
            AccomodationView accommodationView = new AccomodationView(_user);
            accommodationView.ShowDialog();
        }

        private void RequestsOverview_Click(object sender, RoutedEventArgs e)
        {
            RequestOverview requestOverview = new RequestOverview(_user);
            requestOverview.ShowDialog();
        }

        private void OwnerRating_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
