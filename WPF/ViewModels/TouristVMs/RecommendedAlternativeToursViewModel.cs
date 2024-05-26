using BookingApp.Domain.Model;
using BookingApp.View.TouristApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class RecommendedAlternativeToursViewModel
    {
        public ObservableCollection<TourInstance> TourInstances { get; set; }

        public TourInstance SelectedTour { get; set; }

        public User LoggedInUser { get; set; }

        public ObservableCollection<GiftCard> UserGiftCards { get; set; }
        private readonly MainViewModel _mainViewModel;
        public ICommand ReserveTourCommand {  get; set; }

        private readonly IDialogService _dialogService;
        public ICommand BackButtonCommand { get; set; }


        public RecommendedAlternativeToursViewModel(MainViewModel mainViewModel, ObservableCollection<TourInstance> tourInstances, User loggedInUser, ObservableCollection<GiftCard> userGiftCards, IDialogService dialogService)
        {
            TourInstances = tourInstances;
            LoggedInUser = loggedInUser;
            UserGiftCards = userGiftCards;
            _mainViewModel = mainViewModel;
            ReserveTourCommand = new RelayCommand(ReserveTour);
            _dialogService = dialogService;
            BackButtonCommand = new RelayCommand(GoBack);
        }

        public void GoBack()
        {
            _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, new DialogService()));
        }

        private void ReserveTour()
        {
            //NumberOfTouristInsertion numberOfTouristInsertion = new NumberOfTouristInsertion(SelectedTour, TourInstances, LoggedInUser, UserGiftCards);
            //numberOfTouristInsertion.Show();
        }
    }
}
