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
    internal class KeyPointRepository : SubjectNotifier
    {
        private const string FilePath = "../../../Resources/Data/keyPoints.csv";

        private readonly Serializer<KeyPoint> _serializer;

        private List<KeyPoint> _keyPoints;

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






    }
}
