using BookingApp.Domain.Model;

namespace BookingApp.Services.IServices
{
    public interface ITourRequestAcceptanceNotificationService
    {
        public TourRequestAcceptanceNotification Save(TourRequestAcceptanceNotification notification);

        public TourRequestAcceptanceNotification GetById(int id);
    }
}
