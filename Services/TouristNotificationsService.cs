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

        public TourCreationNotificationService _tourCreationNotificationService;

        public TourRequestService _tourRequestService;
        public TouristNotificationsService(ITouristNotificationsRepository touristNotificationsRepository, ITourCreationNotificationRepository tourCreationNotificationRepository, ITourRequestRepository tourRequestRepository) { 
            _touristNotificationsRepository = touristNotificationsRepository;
            _tourCreationNotificationService = new TourCreationNotificationService(tourCreationNotificationRepository);
            _tourRequestService = new TourRequestService(tourRequestRepository);
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

        public void NotifyUser(Tour savedTour, TourInstance savedTourInstance, TourCreationNotification newTourCreationNotification)
        {
            if (newTourCreationNotification.IsBasedOnLanguage)
            {
                List<int> userIds = _tourRequestService.FindUserIdsByLanguage(savedTour.Language);
                foreach (int userId in userIds)
                {
                    newTourCreationNotification.CreatedTourInstanceId = savedTourInstance.Id;
                    TourCreationNotification savedNotification = _tourCreationNotificationService.Save(newTourCreationNotification);
                    TouristNotifications notification = new TouristNotifications(savedNotification.Id, "A tour based on a language you requested has been created. Click \"See more\" to see more info about tour", NotificationType.TourCreation, userId);
                    Save(notification);
                }
            }
            else if (newTourCreationNotification.IsBasedOnLocation)
            {
                List<int> userIds = _tourRequestService.FindUserIdsByLocation(savedTour.Location);
                foreach (int userId in userIds)
                {
                    newTourCreationNotification.CreatedTourInstanceId = savedTourInstance.Id;
                    TourCreationNotification savedNotification = _tourCreationNotificationService.Save(newTourCreationNotification);
                    TouristNotifications notification = new TouristNotifications(savedNotification.Id, "A tour based on a location you requested has been created. Click \"See more\" to see more info about tour", NotificationType.TourCreation, userId);                   
                    Save(notification);
                }
            }
        }
    }
}
