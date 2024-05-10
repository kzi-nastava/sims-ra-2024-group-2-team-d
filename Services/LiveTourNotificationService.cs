using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class LiveTourNotificationService
    {
        public LiveTourNotificationRepository LiveTourNotificationRepository { get; set; }
        public LiveTourNotificationService() { 
            LiveTourNotificationRepository = new LiveTourNotificationRepository();
        }

        public LiveTourNotification Save(LiveTourNotification notification)
        {
           return LiveTourNotificationRepository.Save(notification);
        }

        public LiveTourNotification GetById(int id)
        {
            return LiveTourNotificationRepository.GetById(id);
        }
    }
}
