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

        public double GetAverageGradeForTourInstance(int id)
        {
            double averageGrade = 0;
            List<TourReview> tourReviews = GetAllByTourId(id);
            foreach(TourReview tourReview in tourReviews)
            {
                double avg = tourReview.GuideLanguage + tourReview.GuideKnowledge + tourReview.Enjoyability;
                avg = avg / 3;
                averageGrade += avg;
            }
            return averageGrade/tourReviews.Count();
        }

        public bool SuperGuideGradeCheck(List<TourInstance> instances)
        {
            double avg = 0;
            foreach (TourInstance instance in instances)
            {
                avg += GetAverageGradeForTourInstance(instance.Id);
            }
            if(avg/instances.Count() > 4)
            {
                return true;
            }
            else return false;
        }
    }
}
