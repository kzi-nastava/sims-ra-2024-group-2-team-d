using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Services
{
    public class TourReviewService
    {
        public ITourReviewRepository TourReviewRepository { get; set; }

        public TourReviewService(ITourReviewRepository tourReviewRepository)
        {
            TourReviewRepository = tourReviewRepository;
        }

        public TourReview Update(TourReview tourReview)
        {
            return TourReviewRepository.Update(tourReview);
        }

        public List<TourReview> GetAllByTourId(int id)
        {
            return TourReviewRepository.GetAllByTourId(id);
        }

        public TourReview Save(TourReview tourReview)
        {
            return TourReviewRepository.Save(tourReview);
        }
    }
}
