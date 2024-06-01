using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourGiftCardAwardRecorderRepository: ITourGiftCardAwardRecorderRepository
    {
        private const string FilePath = "../../../Resources/Data/tourGiftCardAwardRecorder.csv";

        private readonly Serializer<TourGiftCardAwardRecorder> _serializer;

        private List<TourGiftCardAwardRecorder> _tourGiftCardAwardRecorder;

        public TourGiftCardAwardRecorderRepository()
        {
            _serializer = new Serializer<TourGiftCardAwardRecorder>();
            _tourGiftCardAwardRecorder = _serializer.FromCSV(FilePath);
        }

        public TourGiftCardAwardRecorder Save(TourGiftCardAwardRecorder tourGiftCardAwardRecorder)
        {
            tourGiftCardAwardRecorder.Id = NextId();
            _tourGiftCardAwardRecorder = _serializer.FromCSV(FilePath);
            _tourGiftCardAwardRecorder.Add(tourGiftCardAwardRecorder);
            _serializer.ToCSV(FilePath, _tourGiftCardAwardRecorder);
            return tourGiftCardAwardRecorder;
        }

        public int NextId()
        {
            _tourGiftCardAwardRecorder = _serializer.FromCSV(FilePath);
            if (_tourGiftCardAwardRecorder.Count < 1)
            {
                return 1;
            }
            return _tourGiftCardAwardRecorder.Max(a => a.Id) + 1;
        }

        public List<TourGiftCardAwardRecorder> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public TourGiftCardAwardRecorder GetByUserId(int userId)
        {
            return _tourGiftCardAwardRecorder.Find(t => t.TouristUserId == userId);
        }

        public TourGiftCardAwardRecorder Update(TourGiftCardAwardRecorder recorder)
        {
            _tourGiftCardAwardRecorder = _serializer.FromCSV(FilePath);
            TourGiftCardAwardRecorder current = _tourGiftCardAwardRecorder.Find(c => c.Id == recorder.Id);
            int index = _tourGiftCardAwardRecorder.IndexOf(current);
            _tourGiftCardAwardRecorder.Remove(current);
            _tourGiftCardAwardRecorder.Insert(index, recorder);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourGiftCardAwardRecorder);
            return recorder;
        }

    }
}
