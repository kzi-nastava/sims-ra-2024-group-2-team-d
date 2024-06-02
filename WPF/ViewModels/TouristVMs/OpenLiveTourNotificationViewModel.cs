using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class OpenLiveTourNotificationViewModel: IRequestClose
    {
        public LiveTourNotificationService LiveTourNotificationService { get; set; }
        public List<Tourist> Tourists { get; set; }

        public TourInstance LiveTour { get; set; }
        public LiveTourNotification Notification { get; set; }
        public ICommand GoBackCommand { get; set; }
        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;

        public OpenLiveTourNotificationViewModel(TouristNotifications notification)
        {
            LiveTourNotificationService = new LiveTourNotificationService(Injector.Injector.CreateInstance<ILiveTourNotificationRepository>());
            Notification = LiveTourNotificationService.GetById(notification.NotificationId);
            TouristService touristService = new TouristService(Injector.Injector.CreateInstance<ITouristRepository>());
            Tourists = touristService.GetByIds(Notification.TouristsId);
            TourInstanceService tourInstanceService = new TourInstanceService(Injector.Injector.CreateInstance<ITourInstanceRepository>(), Injector.Injector.CreateInstance<ITourRepository>(), Injector.Injector.CreateInstance<IKeyPointRepository>(), Injector.Injector.CreateInstance<IPictureRepository>());
            LiveTour = tourInstanceService.GetById(Notification.TourInstanceId);
            GoBackCommand = new RelayCommand(Close);
        }

        public void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));

        }
    }
}
