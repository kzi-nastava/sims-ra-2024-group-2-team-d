using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourCreationNotificationRepository
    {
        public List<TourCreationNotification> GetAll();

        public TourCreationNotification Save(TourCreationNotification notification);

        public TourCreationNotification GetById(int id);

    }
}
