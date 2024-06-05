using BookingApp.CustomClasses;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        List<Notification> GetAll();
        Notification Save(Notification notification);
        int NextId();
        void Delete(Notification notification);
        Notification Update(Notification notification);
    }
}
