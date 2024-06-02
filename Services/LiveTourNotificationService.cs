using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;

namespace BookingApp.Services
{
    public class LiveTourNotificationService: ILiveTourNotificationService
    {
        public ILiveTourNotificationRepository LiveTourNotificationRepository { get; set; }
        public LiveTourNotificationService()
        {
            LiveTourNotificationRepository =  Injector.Injector.CreateInstance<ILiveTourNotificationRepository>();
        }

        public LiveTourNotification Save(LiveTourNotification notification)
        {
            return LiveTourNotificationRepository.Save(notification);
        }

        public LiveTourNotification GetById(int id)
        {
            return LiveTourNotificationRepository.GetById(id);
        }
    }
}
