using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IAccommodationReviewService
    {

         List<AccommodationReview> GetAccommodationReviews(List<Accommodation> accomodations, List<Reservation> reservations);
    }
}
