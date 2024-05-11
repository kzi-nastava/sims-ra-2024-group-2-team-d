using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class OpenLiveTourNotificationViewModel
    {
        public LiveTourNotificationService LiveTourNotificationService { get; set; }
        public List<Tourist> Tourists {  get; set; }

        public TourInstance LiveTour {  get; set; }
        public LiveTourNotification Notification { get; set; }
        public OpenLiveTourNotificationViewModel(TouristNotifications notification) {
            LiveTourNotificationService = new LiveTourNotificationService();
            Notification = LiveTourNotificationService.GetById(notification.NotificationId);
            TouristService touristService = new TouristService();
            Tourists = touristService.GetByIds(Notification.TouristsId);
            TourInstanceService tourInstanceService = new TourInstanceService();
            LiveTour = tourInstanceService.GetById(Notification.TourInstanceId);

        }
    }
}
