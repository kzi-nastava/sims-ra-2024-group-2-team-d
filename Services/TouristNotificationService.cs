using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TouristNotificationService
    {
        private TouristNotificationsRepository _touristNotificationsRepository;
        public TouristNotificationService() { 
            _touristNotificationsRepository = new TouristNotificationsRepository();
        }

        public TouristNotifications ChangeIsReadStatus(TouristNotifications notification)
        {
            return _touristNotificationsRepository.UpdateIsReadStatus(notification);
        }

        public TouristNotifications Save(TouristNotifications notification)
        {
            return _touristNotificationsRepository.Save(notification);
        }
    }
}
