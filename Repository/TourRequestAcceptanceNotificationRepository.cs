using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRequestAcceptanceNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequestAcceptanceNotifications.csv";

        private readonly Serializer<TourRequestAcceptanceNotification> _serializer;

        private List<TourRequestAcceptanceNotification> _tourRequestAcceptanceNotification;

        public TourRequestAcceptanceNotificationRepository()
        {
            _serializer = new Serializer<TourRequestAcceptanceNotification>();
            _tourRequestAcceptanceNotification = _serializer.FromCSV(FilePath);
        }

        public List<TourRequestAcceptanceNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRequestAcceptanceNotification Save(TourRequestAcceptanceNotification notification)
        {
            notification.Id = NextId();
            _tourRequestAcceptanceNotification = _serializer.FromCSV(FilePath);
            _tourRequestAcceptanceNotification.Add(notification);
            _serializer.ToCSV(FilePath, _tourRequestAcceptanceNotification);
            return notification;
        }

        public int NextId()
        {
            _tourRequestAcceptanceNotification = _serializer.FromCSV(FilePath);
            if (_tourRequestAcceptanceNotification.Count < 1)
            {
                return 1;
            }
            return _tourRequestAcceptanceNotification.Max(c => c.Id) + 1;
        }

        public TourRequestAcceptanceNotification GetById(int id)
        {
            return _tourRequestAcceptanceNotification.Find(t => t.Id == id);
        }
    }
}
