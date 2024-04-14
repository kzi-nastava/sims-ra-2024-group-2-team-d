using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class TourInstanceRepository
    {
        private const string FilePath = "../../../Resources/Data/tourInstance.csv";

        private readonly Serializer<TourInstance> _serializer;

        private List<TourInstance> _tourInstance;

        //public DateTime TodayDate = DateTime.Today; 

        public TourInstanceRepository()
        {
            _serializer = new Serializer<TourInstance>();
            _tourInstance = _serializer.FromCSV(FilePath);
        }

        public List<TourInstance> GetAll()
        {
            return _tourInstance;
        }

        public TourInstance UpdateFreeSpots(TourInstance tourInstance)
        {

            TourInstance oldTourInstance = GetById(tourInstance.Id);
            if (oldTourInstance == null) return null;
            oldTourInstance.EmptySpots = tourInstance.EmptySpots;
            _serializer.ToCSV(FilePath, _tourInstance);
            return oldTourInstance;
        }

        public TourInstance UpdateReviewStatus(TourInstance tourInstance)
        {
            TourInstance oldTourInstance = GetById(tourInstance.Id);
            if (oldTourInstance == null) return null;
            oldTourInstance.IsNotReviewed = tourInstance.IsNotReviewed;
            _serializer.ToCSV(FilePath, _tourInstance);
            return oldTourInstance;
        }

        public TourInstance GetById(int id)
        {
            return _tourInstance.Find(x => x.Id == id);
        }

        public TourInstance Save(TourInstance tourInstance)
        {
            tourInstance.Id = NextId();
            _tourInstance = _serializer.FromCSV(FilePath);
            _tourInstance.Add(tourInstance);
            _serializer.ToCSV(FilePath, _tourInstance);
            return tourInstance;
        }

        public int NextId()
        {
            _tourInstance = _serializer.FromCSV(FilePath);
            if (_tourInstance.Count < 1)
            {
                return 1;
            }
            return _tourInstance.Max(c => c.Id) + 1;
        }

        public void Delete(TourInstance tourInstance)
        {
            _tourInstance = _serializer.FromCSV(FilePath);
            TourInstance founded = _tourInstance.Find(c => c.Id == tourInstance.Id);
            _tourInstance.Remove(founded);
            _serializer.ToCSV(FilePath, _tourInstance);
        }

        //ISPRAVNA
        public List<TourInstance> GetForTheDay1(User user, ObservableCollection<TourInstance> tours)
        {
            return tours.Where(c => c.BaseTour.UserId == user.Id && c.Date.Date == DateTime.Today).ToList();
        }


        public List<TourInstance> GetByUser(User user, ObservableCollection<TourInstance> tours)
        {
            return tours.Where(c => c.BaseTour.UserId == user.Id).ToList();
        }

        public List<TourInstance> GetAllFinishedByUser(User user, ObservableCollection<TourInstance> tours)
        {
            return tours.Where(c => c.BaseTour.UserId == user.Id && c.End == true).ToList();
        }

    }
}
