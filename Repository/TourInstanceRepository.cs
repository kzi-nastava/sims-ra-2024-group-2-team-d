using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourInstanceRepository
    {
        private const string FilePath = "../../../Resources/Data/tourInstance.csv";

        private readonly Serializer<TourInstance> _serializer;

        private List<TourInstance> _tourInstance;

        public TourInstanceRepository()
        {
            _serializer = new Serializer<TourInstance>();
            _tourInstance = _serializer.FromCSV(FilePath);
        }

        public List<TourInstance> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourInstance UpdateFreeSpots(TourInstance tourInstance)
        {
            
                TourInstance oldTourInstance = GetById(tourInstance.Id);
                if (oldTourInstance == null) return null;               
                oldTourInstance.EmptySpots = tourInstance.EmptySpots;
                _serializer.ToCSV(FilePath, _tourInstance);               
                return oldTourInstance;         
        }

        public TourInstance GetById(int id)
        {
            return _tourInstance.Find(x=>x.Id==id);
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
    }
}
