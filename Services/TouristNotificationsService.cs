using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class TouristNotificationsService : ITouristNotificationsService
    {
        public ITouristNotificationsRepository _touristNotificationsRepository;

        public ITourCreationNotificationService _tourCreationNotificationService;

        public ITourRequestService _tourRequestService;
        public TouristNotificationsService()
        {
            _touristNotificationsRepository =  Injector.Injector.CreateInstance<ITouristNotificationsRepository>();
            _tourCreationNotificationService =  Injector.Injector.CreateInstance<ITourCreationNotificationService>();
            _tourRequestService = Injector.Injector.CreateInstance<ITourRequestService>();
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
