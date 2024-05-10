using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class FollowingTourLiveViewModel
    {
        public TourInstance ActiveTour { get; set; }

        public KeyPoint CurrentKeyPoint { get; set; }
        public FollowingTourLiveViewModel(TourInstance activeTour) {
            ActiveTour = activeTour;
            GetCurrentPosition();
        }

        public void GetCurrentPosition()
        {
            FollowingTourLiveRepository _followingTourLiveRepository = new FollowingTourLiveRepository();
            FollowingTourLive currentPosition = _followingTourLiveRepository.GetByTourInstanceId(ActiveTour.Id).LastOrDefault();
            KeyPointRepository _keyPointRepository = new KeyPointRepository();
            CurrentKeyPoint = _keyPointRepository.GetById(currentPosition.KeyPointId);
            
        }
    }
}
