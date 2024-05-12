using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Services;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModels
{
    public class TouristNotificationViewModel
    {
        private TouristRepository _touristRepository;

        private FollowingTourLiveService _followingTourLiveService;
        private TourReservationService _tourReservationService;

        public ObservableCollection<List<Tourist>> PresentTourists {  get; set; }
        public User LoggedInUser { get; set; }

        public Tourist UserTourist {  get; set; }
        public TouristNotificationViewModel(List<TourInstance> activeTours, User loggedInUser) {

            _touristRepository = new TouristRepository();
            _followingTourLiveService = new FollowingTourLiveService(Injector.Injector.CreateInstance<IFollowingTourLiveRepository>());
            _tourReservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>(), Injector.Injector.CreateInstance<ITouristRepository>());
            PresentTourists = new ObservableCollection<List<Tourist>>();
            LoggedInUser = loggedInUser;
            CheckForNotification(activeTours);

        }

        public void CheckForNotification(List<TourInstance> activeTours)
        {
            List<int> userToursIds = FilterTours();
            foreach (int userTourId in userToursIds)
            {
                List<Tourist> tourists = new List<Tourist>();
                TourReservation reservation = _tourReservationService.GetByUserAndTourInstanceId(userTourId, LoggedInUser.Id);
                Tourist userTourist = _touristRepository.GetByUserAndReservationId(LoggedInUser.Id, reservation.Id);
                FollowingTourLive followingTourLive = _followingTourLiveService.GetByTouristAndTourInstanceId(userTourist.Id, userTourId);
                if (followingTourLive != null && !userTourist.IsNotified && userTourist.ShowedUp)
                {
                    foreach (FollowingTourLive following in _followingTourLiveService.GetByTourInstanceId(userTourId))
                    {
                        tourists.AddRange(_touristRepository.GetByIds(following.TouristsIds));
                    }
                    PresentTourists.Add(tourists);
                }
               
            }
        }

        public List<int> FilterTours()
        {
            List<int> userToursIds = new List<int>();
            foreach(TourReservation tourReservation in _tourReservationService.GetByUserId(LoggedInUser.Id))
            {
                userToursIds.Add(tourReservation.TourInstanceId);
            }
            return userToursIds;
        }
    }
}
