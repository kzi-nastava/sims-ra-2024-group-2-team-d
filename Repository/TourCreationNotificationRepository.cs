using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourCreationNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourCreationNotifications.csv";

        private readonly Serializer<TourCreationNotification> _serializer;

        private List<TourCreationNotification> _tourCreationNotification;

        public TourCreationNotificationRepository()
        {
            _serializer = new Serializer<TourCreationNotification>();
            _tourCreationNotification = _serializer.FromCSV(FilePath);
        }

        public List<TourCreationNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourCreationNotification Save(TourCreationNotification notification)
        {
            notification.Id = NextId();
            _tourCreationNotification = _serializer.FromCSV(FilePath);
            _tourCreationNotification.Add(notification);
            _serializer.ToCSV(FilePath, _tourCreationNotification);
            return notification;
        }

        public int NextId()
        {
            _tourCreationNotification = _serializer.FromCSV(FilePath);
            if (_tourCreationNotification.Count < 1)
            {
                return 1;
            }
            return _tourCreationNotification.Max(c => c.Id) + 1;
        }

        public TourCreationNotification GetById(int id)
        {
            return _tourCreationNotification.Find(t => t.Id == id);
        }
    }
}
