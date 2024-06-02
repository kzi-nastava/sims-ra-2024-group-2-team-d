using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IGuestReviewService
    {
        public List<GuestReview> GetGuestReviews(List<Reservation> reservations);
    }
}
