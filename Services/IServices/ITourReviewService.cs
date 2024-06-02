using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface ITourReviewService
    {

        public TourReview Update(TourReview tourReview);

        public List<TourReview> GetAllByTourId(int id);

        public TourReview Save(TourReview tourReview);

        public double GetAverageGradeForTourInstance(int id);

        public bool SuperGuideGradeCheck(List<TourInstance> instances);
    }
}
