using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class GuestReviewService
    {
        public GuestReviewRepository _repository { get; set; }

        public GuestReviewService()
        {

            _repository = new GuestReviewRepository();
        }

        public List<GuestReview> GetGuestReviews(List<Reservation> reservations)
        {
            var reservationIds = reservations.Select(obj => obj.Id).ToList();
            return _repository.getGuestReviews().Where(a => reservationIds.Contains(a.ReservationId)).ToList();
        }

    }
}
