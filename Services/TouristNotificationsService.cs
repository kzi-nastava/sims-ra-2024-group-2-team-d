using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;

namespace BookingApp.Services
{
    public class TouristNotificationsService
    {
        public ITouristNotificationsRepository _touristNotificationsRepository;
        public TouristNotificationsService(ITouristNotificationsRepository touristNotificationsRepository) { 
            _touristNotificationsRepository = touristNotificationsRepository;
        }

        public TouristNotifications Save(TouristNotifications touristNotification)
        {
           return _touristNotificationsRepository.Save(touristNotification);
        }

        public List<TouristNotifications> GetByUserId(int userId)
        {
            return _touristNotificationsRepository.GetByUserId(userId);
        }

        public TouristNotifications? ChangeIsReadStatus(TouristNotifications touristNotification)
        {
            return _touristNotificationsRepository.UpdateIsReadStatus(touristNotification);
        }

        public TouristNotifications GetById(int id)
        {
            return _touristNotificationsRepository.GetById(id);
        }
    }
}
