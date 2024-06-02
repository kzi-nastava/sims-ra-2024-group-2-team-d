using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestReviewRepository
    {
        List<GuestReview> GetGuestReviews();
        public List<GuestReview> GetAll();

        public GuestReview GetById(int id);

        public GuestReview Save(GuestReview guestReview);

        public int NextId();

        public void Delete(GuestReview guestReview);

        public GuestReview Update(GuestReview guestReview);
    }
}
