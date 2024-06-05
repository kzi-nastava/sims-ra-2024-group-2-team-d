using BookingApp.CustomClasses;

namespace BookingApp.Services.IServices
{
    public interface INotificationService
    {
        Notification SaveNotification(Notification notification);

        bool IsReservationCanceled(int reservationId);
    }
}
