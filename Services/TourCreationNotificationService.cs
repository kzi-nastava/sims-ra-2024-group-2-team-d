using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Services
{
    public class TourCreationNotificationService
    {
        private ITourCreationNotificationRepository _tourCreationNotificationRepository;
        public TourCreationNotificationService(ITourCreationNotificationRepository tourCreationNotificationRepository) {
            _tourCreationNotificationRepository = tourCreationNotificationRepository;
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
