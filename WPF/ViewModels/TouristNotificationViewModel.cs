using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class TouristNotificationViewModel
    {
        private TouristRepository _touristRepository;

        private FollowingTourLiveRepository _followingTourLiveRepository;
        private TourReservationRepository _tourReservationRepository;

        public ObservableCollection<List<Tourist>> PresentTourists {  get; set; }
        public User LoggedInUser { get; set; }

        public Tourist UserTourist {  get; set; }
        public TouristNotificationViewModel(List<TourInstance> activeTours, User loggedInUser) {

            _touristRepository = new TouristRepository();
            _followingTourLiveRepository = new FollowingTourLiveRepository();
            _tourReservationRepository = new TourReservationRepository();
            PresentTourists = new ObservableCollection<List<Tourist>>();
            LoggedInUser = loggedInUser;
            CheckForNotification(activeTours);

        }

        public void CheckForNotification(List<TourInstance> activeTours)
        {
            foreach (TourInstance activeTour in activeTours)
            {
                List<Tourist> tourists = new List<Tourist>();
                TourReservation reservation = _tourReservationRepository.GetByUserAndTourInstanceId(activeTour.Id, LoggedInUser.Id);
                Tourist userTourist = _touristRepository.GetByUserAndReservationId(LoggedInUser.Id, reservation.Id);
                FollowingTourLive followingTourLive = _followingTourLiveRepository.GetByTouristAndTourInstanceId(userTourist.Id, activeTour.Id);
                if (followingTourLive != null && !userTourist.IsNotified)
                {
                    foreach (FollowingTourLive following in _followingTourLiveRepository.GetByTourInstanceId(activeTour.Id))
                    {
                        tourists.AddRange(_touristRepository.GetByIds(following.TouristsIds));
                    }
                }
                PresentTourists.Add(tourists);
            }
        }
    }
}
