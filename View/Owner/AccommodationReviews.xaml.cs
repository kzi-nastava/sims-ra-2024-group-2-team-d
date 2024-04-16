using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace BookingApp.View.Owner
{
    public partial class AccommodationReviews : Window
    {
        private readonly User _user;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly AccommodationReviewRepository _accommodationReviewRepository;
        private List<Reservation> _reservations;
        private List<Accommodation> _accommodations;
        public ObservableCollection<AccommodationReview> Reviews { get; set; }
        public string AverageRatingText { get { return $"Prosečna ocena: {CalculateAverageRating()}"; } }
        public string SuperOwnerStatusText { get { return IsSuperOwner() ? "Vlasnik je super-vlasnik!" : "Vlasnik nije super-vlasnik."; } }

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
            Reviews = new ObservableCollection<AccommodationReview>(_accommodationReviewRepository.GetAccommodationReviews(_accommodations, _reservations));
        }

        private double CalculateAverageRating()
        {
            if (Reviews == null || Reviews.Count == 0)
                return 0;

            double totalRating = 0;
            foreach (var review in Reviews)
            {
                // Računanje prosečne ocene za svaku recenziju
                double averageRating = (review.Correctness + review.Cleanness) / 2;
                totalRating += averageRating;
            }

            // Računanje prosečne ocene za sve recenzije
            return totalRating / Reviews.Count;
        }

        private bool IsSuperOwner()
        {
            double averageRating = CalculateAverageRating();
            return Reviews.Count >= 50 && averageRating > 4.5;
        }
    }
}