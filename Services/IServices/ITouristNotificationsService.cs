using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface ITouristNotificationsService
    {
        public TouristNotifications Save(TouristNotifications touristNotification);

        public List<TouristNotifications> GetByUserId(int userId);

        public TouristNotifications? ChangeIsReadStatus(TouristNotifications touristNotification);

        public TouristNotifications GetById(int id);

        public void NotifyUser(Tour savedTour, TourInstance savedTourInstance, TourCreationNotification newTourCreationNotification);
    }
}
