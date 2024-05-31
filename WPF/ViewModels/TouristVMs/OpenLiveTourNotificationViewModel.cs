using BookingApp.Domain.Model;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class OpenLiveTourNotificationViewModel
    {
        public ILiveTourNotificationService LiveTourNotificationService { get; set; }
        public List<Tourist> Tourists { get; set; }

        public TourInstance LiveTour { get; set; }
        public LiveTourNotification Notification { get; set; }
        public OpenLiveTourNotificationViewModel(TouristNotifications notification)
        {
            LiveTourNotificationService = Injector.Injector.CreateInstance<ILiveTourNotificationService>();
            Notification = LiveTourNotificationService.GetById(notification.NotificationId);
            ITouristService touristService = Injector.Injector.CreateInstance<ITouristService>();
            Tourists = touristService.GetByIds(Notification.TouristsId);
            var tourInstanceService = Injector.Injector.CreateInstance<ITourInstanceService>();
            LiveTour = tourInstanceService.GetById(Notification.TourInstanceId);

        }
    }
}
