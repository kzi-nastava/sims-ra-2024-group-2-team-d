using BookingApp.Domain.Model;

namespace BookingApp.Services.IServices
{
    public interface ITourCreationNotificationService
    {
        public TourCreationNotification Save(TourCreationNotification tourCreationNotification);

        public TourCreationNotification GetById(int id);
    }
}
