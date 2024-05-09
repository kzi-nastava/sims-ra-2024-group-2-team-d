using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels
{
    public class UserToursViewModel
    {
        public static ObservableCollection<TourInstance> ReservedTours { get; set; }
        public static ObservableCollection<TourInstance> FinishedTours { get; set; }
        public User LoggedInUser { get; set; }
        public static TourInstance SelectedTour { get; set; }

        public ICommand OpenTourReviewCommand { get; } 

       public UserToursViewModel(User loggedInUser, ObservableCollection<TourInstance> tourInstances) {
            ReservedTours = new ObservableCollection<TourInstance>();
            FinishedTours = new ObservableCollection<TourInstance>();
            LoggedInUser = loggedInUser;
            OpenTourReviewCommand = new RelayCommand(OpenTourReview);
            FilterTours(tourInstances);
        }

        public void FilterTours(ObservableCollection<TourInstance> tourInstances)
        {
            TourReservationRepository _tourReservationRepository = new TourReservationRepository();
            List<TourReservation> tourReservations = _tourReservationRepository.GetAll();
            List<TourInstance> tourInstanceList = tourInstances.ToList();
            TouristRepository _touristsRepository = new TouristRepository();
            List<Tourist> tourists = _touristsRepository.GetAll();
            foreach(TourReservation tourReservation in tourReservations)
            {
                AddToCorrespondingTour(tourReservation, tourists, tourInstanceList);
            }
        }

        public void AddToCorrespondingTour(TourReservation tourReservation, List<Tourist> tourists, List<TourInstance> tourInstanceList)
        {
            if (tourReservation.UserId != LoggedInUser.Id) return;         
                var matchingTourInstance = tourInstanceList.Find(tourInstance => tourInstance.Id == tourReservation.TourInstanceId && (tourInstance.End || !tourInstance.Start));
                var matchingTourist = tourists.Find(tourist => tourist.ReservationId == tourReservation.Id && tourist.UserId == LoggedInUser.Id);
            if (matchingTourist == null || matchingTourInstance == null) return;             
                    if (matchingTourInstance.End && matchingTourist.ShowedUp)
                    {
                        FinishedTours.Add(matchingTourInstance);
                    }
                    else if (!matchingTourInstance.Start)
                    {
                        ReservedTours.Add(matchingTourInstance);
                    }                     
        }

        private void OpenTourReview()
        {
            UserTourReviewView userTourReviewView = new UserTourReviewView(LoggedInUser, SelectedTour);
            userTourReviewView.Show();
        }
    }
}
