using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILiveTourNotificationRepository
    {
        public LiveTourNotification Save(LiveTourNotification liveTourNotification);

        public LiveTourNotification GetById(int id);
    }
}
