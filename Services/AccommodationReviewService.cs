using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Injector;
using BookingApp.Repository;
using BookingApp.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services
{
    public class AccommodationReviewService : IAccommodationReviewService
    {
        public IAccommodationReviewRepository _repository { get; set; }

        public AccommodationReviewService()
        {

            _repository = Injector.Injector.CreateInstance<IAccommodationReviewRepository>();
        }

        public List<AccommodationReview> GetAccommodationReviews(List<Accommodation> accomodations, List<Reservation> reservations)
        {
            var accomodationIds = accomodations.Select(obj => obj.Id).ToList();
            var reservationIds = reservations.Select(obj => obj.Id).ToList();
            return _repository.getAccomReviews().Where(a => accomodationIds.Contains(a.AccomodationId) && reservationIds.Contains(a.ReservationId)).ToList();
        }

    }
}
