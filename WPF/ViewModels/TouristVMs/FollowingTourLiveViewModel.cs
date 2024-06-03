using BookingApp.Domain.Model;
using BookingApp.Services.IServices;
using System;
using System.Linq;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class FollowingTourLiveViewModel: IRequestClose
    {
        public TourInstance ActiveTour { get; set; }
        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;

        public KeyPoint CurrentKeyPoint { get; set; }
        public ICommand CloseCommand {  get; set; }
        public FollowingTourLiveViewModel(TourInstance activeTour)
        {
            ActiveTour = activeTour;
            GetCurrentPosition();
            CloseCommand = new RelayCommand(Close);
        }


        public void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }

        public void GetCurrentPosition()
        {
            IFollowingTourLiveService _followingTourLiveService = Injector.Injector.CreateInstance<IFollowingTourLiveService>();
            FollowingTourLive currentPosition = _followingTourLiveService.GetByTourInstanceId(ActiveTour.Id).LastOrDefault();
            IKeyPointService _keyPointService = Injector.Injector.CreateInstance<IKeyPointService>();
            CurrentKeyPoint = _keyPointService.GetById(currentPosition.KeyPointId);

        }
    }
}
