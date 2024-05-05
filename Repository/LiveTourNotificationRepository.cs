using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class LiveTourNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/liveTourNotifications.csv";

        private readonly Serializer<LiveTourNotification> _serializer;

        private List<LiveTourNotification> _liveTourNotification;

        public LiveTourNotificationRepository()
        {
            _serializer = new Serializer<LiveTourNotification>();
            _liveTourNotification = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _liveTourNotification = _serializer.FromCSV(FilePath);
            if (_liveTourNotification.Count < 1)
            {
                return 1;
            }
            return _liveTourNotification.Max(c => c.Id) + 1;
        }

        public LiveTourNotification Save(LiveTourNotification liveTourNotification)
        {
            liveTourNotification.Id = NextId();
            _liveTourNotification = _serializer.FromCSV(FilePath);
            _liveTourNotification.Add(liveTourNotification);
            _serializer.ToCSV(FilePath, _liveTourNotification);
            return liveTourNotification;
        }

        public LiveTourNotification GetById(int id)
        {
            return _liveTourNotification.Find(x => x.Id == id);
        }

    }
}
