using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourCreationNotificationService
    {
        private TourCreationNotificationRepository _tourCreationNotificationRepository;
        public TourCreationNotificationService() {
            _tourCreationNotificationRepository = new TourCreationNotificationRepository();
        }

        public TourCreationNotification Save(TourCreationNotification tourCreationNotification)
        {
           return _tourCreationNotificationRepository.Save(tourCreationNotification);
        }

        public TourCreationNotification GetById(int id)
        {
            return _tourCreationNotificationRepository.GetById(id);
        }
    }
}
