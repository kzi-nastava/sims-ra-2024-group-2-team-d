using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Services
{
    public class TourRequestAcceptanceNotificationService
    {
        private ITourRequestAcceptanceNotificationRepository _tourRequestAcceptanceNotificationRepository;

        public TourRequestAcceptanceNotificationService(ITourRequestAcceptanceNotificationRepository tourRequestAcceptanceNotificationRepository)
        {
            _tourRequestAcceptanceNotificationRepository = tourRequestAcceptanceNotificationRepository;
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
