using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationReviews.xaml
    /// </summary>
    public partial class AccommodationReviews : Window
    {
        private readonly User _user;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly AccommodationReviewRepository _accommodationReviewRepository;
        private List<Reservation> _reservations;
        private List<Accommodation> _accommodations;
        public ObservableCollection<AccommodationReview> Reviews { get; set; }
        public AccommodationReviews(User user)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _accommodationRepository = new AccommodationRepository();
            _reservationRepository = new ReservationRepository();
            _accommodationReviewRepository = new AccommodationReviewRepository();
            _reservations = new List<Reservation>(_reservationRepository.GetAllReviewedByBoth());
            _accommodations = new List<Accommodation>(_accommodationRepository.GetAllOwnerAccommodations(user.Id));
            Reviews = new ObservableCollection<AccommodationReview>(_accommodationReviewRepository.GetAccommodationReviews(_accommodations,_reservations));
        }
    }
}
