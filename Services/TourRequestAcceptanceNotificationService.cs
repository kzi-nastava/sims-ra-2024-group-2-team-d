using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;

namespace BookingApp.Services
{
    public class TourRequestAcceptanceNotificationService : ITourRequestAcceptanceNotificationService
    {
        private ITourRequestAcceptanceNotificationRepository _tourRequestAcceptanceNotificationRepository;

        public TourRequestAcceptanceNotificationService()
        {
            _tourRequestAcceptanceNotificationRepository =  Injector.Injector.CreateInstance<ITourRequestAcceptanceNotificationRepository>();
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
