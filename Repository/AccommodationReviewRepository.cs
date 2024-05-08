using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class AccommodationReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationReviews.csv";

        private readonly Serializer<AccommodationReview> _serializer;

        private List<AccommodationReview> _accommodationReviews;
        public List<AccommodationReview> getAccomReviews()
        {
            return _accommodationReviews;
        }
        public AccommodationReviewRepository()
        {
            _serializer = new Serializer<AccommodationReview>();
            _accommodationReviews = _serializer.FromCSV(FilePath);
        }

        public List<AccommodationReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationReview GetById(int id)
        {
            return _serializer.FromCSV(FilePath).Where(a => a.Id == id).FirstOrDefault();
        }

        public AccommodationReview Save(AccommodationReview accommodationReview)
        {
            accommodationReview.Id = NextId();
            _accommodationReviews = _serializer.FromCSV(FilePath);
            _accommodationReviews.Add(accommodationReview);
            _serializer.ToCSV(FilePath, _accommodationReviews);
            return accommodationReview;
        }

        public int NextId()
        {
            _accommodationReviews = _serializer.FromCSV(FilePath);
            if (_accommodationReviews.Count < 1)
            {
                return 1;
            }
            return _accommodationReviews.Max(a => a.Id) + 1;
        }

        public void Delete(AccommodationReview accommodationReview)
        {
            _accommodationReviews = _serializer.FromCSV(FilePath);
            AccommodationReview founded = _accommodationReviews.Find(a => a.Id == accommodationReview.Id);
            _accommodationReviews.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodationReviews);
        }

        public AccommodationReview Update(AccommodationReview accommodationReview)
        {
            _accommodationReviews = _serializer.FromCSV(FilePath);
            AccommodationReview current = _accommodationReviews.Find(a => a.Id == accommodationReview.Id);
            int index = _accommodationReviews.IndexOf(current);
            _accommodationReviews.Remove(current);
            _accommodationReviews.Insert(index, accommodationReview);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _accommodationReviews);
            return accommodationReview;
        }

        public List<AccommodationReview> GetAccommodationReviews(List<Accommodation> accomodations, List<Reservation> reservations)
        {
            var accomodationIds = accomodations.Select(obj => obj.Id).ToList();
            var reservationIds = reservations.Select(obj => obj.Id).ToList();
            return _accommodationReviews.Where(a => accomodationIds.Contains(a.AccomodationId) && reservationIds.Contains(a.ReservationId)).ToList();
        }
    }
}
