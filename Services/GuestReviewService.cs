using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services
{
    public class GuestReviewService : IGuestReviewService
    {
        public IGuestReviewRepository _repository { get; set; }

        public GuestReviewService()
        {

            _repository = Injector.Injector.CreateInstance<IGuestReviewRepository>();
        }

        public List<GuestReview> GetGuestReviews(List<Reservation> reservations)
        {
            var reservationIds = reservations.Select(obj => obj.Id).ToList();
            return _repository.GetGuestReviews().Where(a => reservationIds.Contains(a.ReservationId)).ToList();
        }

    }
}
