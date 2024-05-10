using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;
using BookingApp.Domain.Model;

namespace BookingApp.Services
{
    public class TourRequestAcceptanceNotificationService
    {
        private TourRequestAcceptanceNotificationRepository _tourRequestAcceptanceNotificationRepository;

        public TourRequestAcceptanceNotificationService()
        {
            _tourRequestAcceptanceNotificationRepository = new TourRequestAcceptanceNotificationRepository();
        }

        public TourRequestAcceptanceNotification Save(TourRequestAcceptanceNotification notification)
        {
            return _tourRequestAcceptanceNotificationRepository.Save(notification);
        }

        public TourRequestAcceptanceNotification GetById(int id)
        {
            return _tourRequestAcceptanceNotificationRepository.GetById(id);
        }

    }
}
