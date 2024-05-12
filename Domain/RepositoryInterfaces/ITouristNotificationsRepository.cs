using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristNotificationsRepository
    {
        public TouristNotifications Save(TouristNotifications touristNotification);

        public List<TouristNotifications> GetByUserId(int userId);

        public TouristNotifications? UpdateIsReadStatus(TouristNotifications touristNotification);

        public TouristNotifications GetById(int id);
    }
}
