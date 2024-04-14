using BookingApp.Model;
using BookingApp.View.Owner;
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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private readonly User _user;
        public HomeWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccomodation registerAccomodation = new RegisterAccomodation(_user);
            registerAccomodation.ShowDialog();
        }

        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReviews accommodationReviews = new AccommodationReviews(_user);
            accommodationReviews.ShowDialog();
        }
    }
}
