using BookingApp.Domain.Model;
using BookingApp.Serializer;
using BookingApp.View.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReviewRepository
    {
        public List<AccommodationReview> GetAll();
        public AccommodationReview GetById(int id);
        public AccommodationReview Save(AccommodationReview accommodationReview);

        public int NextId();
        public void Delete(AccommodationReview accommodationReview);

        public AccommodationReview Update(AccommodationReview accommodationReview);

        public List<AccommodationReview> GetAccommodationReviews(List<Accommodation> accomodations, List<Reservation> reservations);

        public List<AccommodationReview> getAccomReviews();


    }
}
