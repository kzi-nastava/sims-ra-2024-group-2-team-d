using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel
{
    public class AccommodationReviewsViewModel
    {
        public ObservableCollection<AccommodationReview> Reviews { get; set; }
        private List<Reservation> _reservations;
        private List<Accommodation> _accommodations;
        public string AverageRatingText { get { return $"Prosečna ocena: {CalculateAverageRating()}"; } }
        public string SuperOwnerStatusText { get { return IsSuperOwner() ? "Vlasnik je super-vlasnik!" : "Vlasnik nije super-vlasnik."; } }

        public BaseService baseService { get; set; }
        private readonly User _user;
        public AccommodationReviewsViewModel(User user)
        {
            _user = user;

            this.baseService =BaseService.getInstance();
            _reservations = new List<Reservation>(baseService.ReservationService.GetAllReviewedByBoth());
            _accommodations = new List<Accommodation>(baseService.AccomodationService.GetAllOwnerAccommodations(user.Id));
            Reviews = new ObservableCollection<AccommodationReview>(baseService.AccommodationReviewService.GetAccommodationReviews(_accommodations, _reservations));

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

