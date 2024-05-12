using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        public TourReview Save(TourReview tourReview);

        public TourReview Update(TourReview tourReview);

        public List<TourReview> GetAllByTourIdAndUser(int id, User user);

        public List<TourReview> GetAllByTourId(int id);
    }
}
