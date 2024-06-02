using BookingApp.Domain.Model;

namespace BookingApp.Services.IServices
{
    public interface ILiveTourNotificationService
    {
        public LiveTourNotification Save(LiveTourNotification notification);

        public LiveTourNotification GetById(int id);
    }
}
