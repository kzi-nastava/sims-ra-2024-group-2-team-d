using BookingApp.Domain.Model;
using BookingApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class OpenLiveTourNotificationViewModel: IRequestClose
    {
        public ILiveTourNotificationService LiveTourNotificationService { get; set; }
        public List<Tourist> Tourists { get; set; }

        public TourInstance LiveTour { get; set; }
        public LiveTourNotification Notification { get; set; }
        public ICommand GoBackCommand { get; set; }
        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;

        public OpenLiveTourNotificationViewModel(TouristNotifications notification)
        {
            LiveTourNotificationService = Injector.Injector.CreateInstance<ILiveTourNotificationService>();
            Notification = LiveTourNotificationService.GetById(notification.NotificationId);
            ITouristService touristService = Injector.Injector.CreateInstance<ITouristService>();
            Tourists = touristService.GetByIds(Notification.TouristsId);
            var tourInstanceService = Injector.Injector.CreateInstance<ITourInstanceService>();
            LiveTour = tourInstanceService.GetById(Notification.TourInstanceId);
            GoBackCommand = new RelayCommand(Close);
        }

        event EventHandler<DialogCloseRequestedEventArgs> IRequestClose.RequestClose
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));

        }
    }
}
