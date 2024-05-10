using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourReviewService
    {
        public TourReviewRepository TourReviewRepository { get; set; }

        public TourReviewService()
        {
            TourReviewRepository = new TourReviewRepository();
        }

        public TourReview Update(TourReview tourReview)
        {
            return TourReviewRepository.Update(tourReview);
        }

        public List<TourReview> GetAllByTourId(int id)
        {
            return TourReviewRepository.GetAllByTourId(id);
        }
    }
}
