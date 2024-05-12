using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
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
        public ILiveTourNotificationRepository LiveTourNotificationRepository { get; set; }
        public LiveTourNotificationService(ILiveTourNotificationRepository liveTourNotificationRepository) { 
            LiveTourNotificationRepository = liveTourNotificationRepository;
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
