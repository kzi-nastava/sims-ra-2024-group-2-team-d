using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Services;
using BookingApp.Domain.RepositoryInterfaces;

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
            FollowingTourLiveService _followingTourLiveService = new FollowingTourLiveService(Injector.Injector.CreateInstance<IFollowingTourLiveRepository>());
            FollowingTourLive currentPosition = _followingTourLiveService.GetByTourInstanceId(ActiveTour.Id).LastOrDefault();
            KeyPointService _keyPointService = new KeyPointService(Injector.Injector.CreateInstance<IKeyPointRepository>());
            CurrentKeyPoint = _keyPointService.GetById(currentPosition.KeyPointId);

        }
    }
}
