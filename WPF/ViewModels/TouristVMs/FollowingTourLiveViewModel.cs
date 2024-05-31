using BookingApp.Domain.Model;
using BookingApp.Services.IServices;
using System.Linq;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class FollowingTourLiveViewModel
    {
        public TourInstance ActiveTour { get; set; }

        public KeyPoint CurrentKeyPoint { get; set; }
        public FollowingTourLiveViewModel(TourInstance activeTour)
        {
            ActiveTour = activeTour;
            GetCurrentPosition();
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
