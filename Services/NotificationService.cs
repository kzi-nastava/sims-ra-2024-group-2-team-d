using BookingApp.CustomClasses;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;

namespace BookingApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRespository;

        public NotificationService()
        {
            _notificationRespository = Injector.Injector.CreateInstance<INotificationRepository>();
        }

        public Notification SaveNotification(Notification notificiation)
        {
            return _notificationRespository.Save(notificiation);
        }

        public bool IsReservationCanceled(int reservationId)
        {
            Notification notification = _notificationRespository.GetAll().Find(r => r.ReservationId == reservationId);
            if (notification != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
