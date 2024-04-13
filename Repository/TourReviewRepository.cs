using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReviews.csv";

        private readonly Serializer<TourReview> _serializer;

        private List<TourReview> _tourReviews;

        public TourReviewRepository()
        {
            _serializer = new Serializer<TourReview>();
            _tourReviews = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            if (_tourReviews.Count < 1)
            {
                return 1;
            }
            return _tourReviews.Max(c => c.Id) + 1;
        }

        public TourReview Save(TourReview tourReview)
        {
            tourReview.Id = NextId();
            _tourReviews = _serializer.FromCSV(FilePath);
            _tourReviews.Add(tourReview);
            _serializer.ToCSV(FilePath, _tourReviews);
            return tourReview;
        }

        public TourReview Update(TourReview tourReview)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            TourReview current = _tourReviews.Find(c => c.Id == tourReview.Id);
            int index = _tourReviews.IndexOf(current);
            _tourReviews.Remove(current);
            _tourReviews.Insert(index, tourReview);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourReviews);
            return tourReview;
        }

        public List<TourReview> GetAllByTourIdAndUser(int id, User user) { 
        
            return _tourReviews.Where(r=>r.TourId==id && r.GuideId ==user.Id).ToList();
        }

        
        
        public List<TourReview> GetAllByTourId(int id)
        {

            return _tourReviews.Where(r => r.TourId == id ).ToList();
        }

    }
}
