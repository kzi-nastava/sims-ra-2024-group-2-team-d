using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class UserToursViewModel
    {
        public static ObservableCollection<TourInstance> ReservedTours { get; set; }
        public static ObservableCollection<TourInstance> FinishedTours { get; set; }
        public User LoggedInUser { get; set; }
        public static TourInstance SelectedTour { get; set; }
        public RelayCommand OpenMorePicturesCommand {  get; set; }
        public ICommand OpenTourReviewCommand { get; set; }
        public IDialogService _dialogService {  get; set; }
        public RelayCommand GoBackCommand {  get; set; }
        public RelayCommand MoreInfoCommand {  get; set; }

        private readonly MainViewModel MainViewModel;
        public static ObservableCollection<TourInstance> TourInstances { get; set; }



        public UserToursViewModel(User loggedInUser, ObservableCollection<TourInstance> tourInstances, IDialogService dialogService, MainViewModel mainViewModel)
        {
            ReservedTours = new ObservableCollection<TourInstance>();
            FinishedTours = new ObservableCollection<TourInstance>();
            LoggedInUser = loggedInUser;
            TourInstances = tourInstances;
            OpenTourReviewCommand = new RelayCommand(tourInstance => OpenTourReview((TourInstance)tourInstance));
            FilterTours(tourInstances);
            OpenMorePicturesCommand = new RelayCommand(tourInstance =>OpenMorePictures((TourInstance)tourInstance));
            _dialogService = dialogService;
            MainViewModel = mainViewModel;
            MoreInfoCommand = new RelayCommand(tourInstance => ShowMoreInfo((TourInstance)tourInstance));
            GoBackCommand = new RelayCommand(GoBack);

        }




        public void ShowMoreInfo(TourInstance tourInstance)
        {
            var viewModel = new MoreInfoAboutTourViewModel(tourInstance, _dialogService);
            bool? result = _dialogService.ShowDialog(viewModel);
        }

        public void GoBack()
        {
            MainViewModel.SwitchView(new TouristHomeViewModel(MainViewModel, LoggedInUser, new DialogService()));
        }

        public void OpenMorePictures(TourInstance tourInstance)
        {
            var viewModel = new ShowMorePicturesViewModel(tourInstance);
            bool? result = _dialogService.ShowDialog(viewModel);
        }

        public void FilterTours(ObservableCollection<TourInstance> tourInstances)
        {
            TourReservationService _tourReservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>(), Injector.Injector.CreateInstance<ITouristRepository>());
            List<TourReservation> tourReservations = _tourReservationService.GetAll();
            List<TourInstance> tourInstanceList = tourInstances.ToList();
            TouristService _touristsService = new TouristService(Injector.Injector.CreateInstance<ITouristRepository>());
            List<Tourist> tourists = _touristsService.GetAll();
            foreach (TourReservation tourReservation in tourReservations)
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

        private void OpenTourReview(TourInstance tourInstance)
        {
            //UserTourReviewView userTourReviewView = new UserTourReviewView(LoggedInUser, tourInstance);
            //userTourReviewView.Show();
            MainViewModel.SwitchView(new UserTourReviewViewModel(LoggedInUser, tourInstance, MainViewModel, _dialogService, TourInstances));
        }
    }
}
