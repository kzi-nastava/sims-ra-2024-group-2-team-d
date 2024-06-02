using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;

namespace BookingApp.Services
{
    public class TourCreationNotificationService : ITourCreationNotificationService
    {
        private ITourCreationNotificationRepository _tourCreationNotificationRepository;
        public TourCreationNotificationService()
        {
            _tourCreationNotificationRepository = Injector.Injector.CreateInstance<ITourCreationNotificationRepository>();
        }

        public TourCreationNotification Save(TourCreationNotification tourCreationNotification)
        {
            return _tourCreationNotificationRepository.Save(tourCreationNotification);
        }

        public TourCreationNotification GetById(int id)
        {
            return _tourCreationNotificationRepository.GetById(id);
        }
    }
}
