using BookingApp.Model;
using BookingApp.Repository;
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
        private readonly ReservationRepository _reservationRepository;
        public Guest1Window(User user)
        {
            InitializeComponent();
            _reservationRepository = new ReservationRepository();
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
            CheckReviewNotifications(_user.Id);
        }

        private void CheckReviewNotifications(int userId)
        {
            var list = _reservationRepository.GetAllUnreviewedByGuest(userId);
            if (list.Count > 0)
            {
                foreach (var r in list)
                {
                    AccommodationReviewForm accommodationReviewForm = new AccommodationReviewForm(r);
                    accommodationReviewForm.Show();
                }
            }

        }
    }
}
