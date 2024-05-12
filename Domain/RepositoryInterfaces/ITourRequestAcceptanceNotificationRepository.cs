using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestAcceptanceNotificationRepository
    {
        public List<TourRequestAcceptanceNotification> GetAll();

        public TourRequestAcceptanceNotification Save(TourRequestAcceptanceNotification notification);


        public TourRequestAcceptanceNotification GetById(int id);
    }
}
