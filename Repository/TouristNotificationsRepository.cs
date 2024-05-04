using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TouristNotificationsRepository
    {
        private const string FilePath = "../../../Resources/Data/touristNotifications.csv";

        private readonly Serializer<TouristNotifications> _serializer;

        private List<TouristNotifications> _touristNotifications;

        public TouristNotificationsRepository()
        {
            _serializer = new Serializer<TouristNotifications>();
            _touristNotifications = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _touristNotifications = _serializer.FromCSV(FilePath);
            if (_touristNotifications.Count < 1)
            {
                return 1;
            }
            return _touristNotifications.Max(c => c.Id) + 1;
        }

        public TouristNotifications Save(TouristNotifications touristNotification)
        {
            touristNotification.Id = NextId();
            _touristNotifications = _serializer.FromCSV(FilePath);
            _touristNotifications.Add(touristNotification);
            _serializer.ToCSV(FilePath, _touristNotifications);
            return touristNotification;
        }

        public List<TouristNotifications> GetByUserId(int userId)
        {
            return _touristNotifications.FindAll(t => t.UserId == userId && !t.IsRead);
        }
    }
}
