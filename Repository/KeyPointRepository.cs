using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class KeyPointRepository : SubjectNotifier
    {
        private const string FilePath = "../../../Resources/Data/keyPoints.csv";

        private readonly Serializer<KeyPoint> _serializer;

        private List<KeyPoint> _keyPoints;

        //public TourInstance TourInstance;

        public KeyPointRepository()
        {
            _serializer = new Serializer<KeyPoint>();
            _keyPoints = _serializer.FromCSV(FilePath);
        }

        public List<KeyPoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            if (_keyPoints.Count < 1)
            {
                return 1;
            }
            return _keyPoints.Max(c => c.Id) + 1;
        }

        public KeyPoint Save(KeyPoint keyPoint)
        {
            keyPoint.Id = NextId();
            _keyPoints = _serializer.FromCSV(FilePath);
            _keyPoints.Add(keyPoint);
            _serializer.ToCSV(FilePath, _keyPoints);
            return keyPoint;
        }

        public KeyPoint GetById(int Id)
        {
            return _keyPoints.Find(x => x.Id == Id);
        }

        public void Delete(KeyPoint keyPoints)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            KeyPoint founded = _keyPoints.Find(c => c.Id == keyPoints.Id);
            _keyPoints.Remove(founded);
            _serializer.ToCSV(FilePath, _keyPoints);
        }

        public KeyPoint Update(KeyPoint keyPoints)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            KeyPoint current = _keyPoints.Find(c => c.Id == keyPoints.Id);
            int index = _keyPoints.IndexOf(current);
            _keyPoints.Remove(current);
            _keyPoints.Insert(index, keyPoints);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _keyPoints);
            return keyPoints;
        }

        public List<KeyPoint> GetByTourId(int tourId)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            return _keyPoints.FindAll(c => c.TourId == tourId);
        }

        public List<KeyPoint> GetByTourInstance(TourInstance tourInstance)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            return _keyPoints.FindAll(c => c.TourId == tourInstance.TourId);
        }

        public string GetKeyPointName(int keyPointId)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
           return _keyPoints.Find(c => c.Id == keyPointId).Name;
           
        }

    }
}
