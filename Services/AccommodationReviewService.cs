using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AccommodationReviewService
    {
        public AccommodationReviewRepository _repository { get; set; }

        public AccommodationReviewService()
        {

            _repository = new AccommodationReviewRepository();
        }

        public List<AccommodationReview> GetAccommodationReviews(List<Accommodation> accomodations, List<Reservation> reservations)
        {
            var accomodationIds = accomodations.Select(obj => obj.Id).ToList();
            var reservationIds = reservations.Select(obj => obj.Id).ToList();
            return _repository.getAccomReviews().Where(a => accomodationIds.Contains(a.AccomodationId) && reservationIds.Contains(a.ReservationId)).ToList();
        }

    }
}
