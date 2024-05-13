using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Services;

namespace BookingApp.ViewModel
{
    public class GuestRatingsViewModel
    {

        public ObservableCollection<GuestReview> _rewiews;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<GuestReview> Reviews
        {
            get { return _rewiews; }
            set
            {
                _rewiews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }




        private List<Reservation> _reservations;
        private List<Accommodation> _accommodations;
        public string AverageRatingText { get { return $"Prosečna ocena: {CalculateAverageRating()}"; } }


        private readonly User _user;
        public BaseService baseService { get; set; }

        public GuestRatingsViewModel(User user)
        {
            _user = user;
            this.baseService = BaseService.getInstance();
            _reservations = new List<Reservation>(baseService.ReservationService.GetAllReviewedByBoth());
            Reviews = new ObservableCollection<GuestReview>(baseService.GuestReviewService.GetGuestReviews(_reservations));

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private double CalculateAverageRating()
        {

            if (Reviews == null || Reviews.Count == 0)
                return 0;

            double totalRating = 0;
            foreach (var review in Reviews)
            {
                double averageRating = (review.Cleanness + review.Rules) / 2;
                totalRating += averageRating;
            }

            return totalRating / Reviews.Count;
        }

    }
}
