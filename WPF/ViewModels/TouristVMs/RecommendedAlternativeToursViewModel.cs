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
        public ICommand ReserveTourCommand { get; set; }

        private readonly IDialogService _dialogService;
        public ICommand BackButtonCommand { get; set; }
        public ICommand MoreInfoCommand { get; set; }


        public RecommendedAlternativeToursViewModel(MainViewModel mainViewModel, ObservableCollection<TourInstance> tourInstances, User loggedInUser, ObservableCollection<GiftCard> userGiftCards, IDialogService dialogService)
        {
            TourInstances = tourInstances;
            LoggedInUser = loggedInUser;
            UserGiftCards = userGiftCards;
            _mainViewModel = mainViewModel;
            ReserveTourCommand = new RelayCommand(tourInstance => ReserveTour((TourInstance)tourInstance));
            _dialogService = dialogService;
            BackButtonCommand = new RelayCommand(GoBack);
            MoreInfoCommand = new RelayCommand(tourInstance => ShowMoreInfo((TourInstance)tourInstance));

        }

        public void ShowMoreInfo(TourInstance tourInstance)
        {
            var viewModel = new MoreInfoAboutTourViewModel(tourInstance,_dialogService);
            bool? result = _dialogService.ShowDialog(viewModel);
            //MoreInfoAboutTourView moreInfoAboutTourView = new MoreInfoAboutTourView(tourInstance);
            //moreInfoAboutTourView.Show();
        }

        public void GoBack()
        {
            _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, new DialogService()));
        }

        private void ReserveTour(TourInstance tourInstance)
        {
            //NumberOfTouristInsertion numberOfTouristInsertion = new NumberOfTouristInsertion(SelectedTour, TourInstances, LoggedInUser, UserGiftCards);
            //numberOfTouristInsertion.Show();
            var viewModel = new NumberOfTouristInsertionViewModel(tourInstance, TourInstances, LoggedInUser, UserGiftCards, _dialogService);
            bool? result = _dialogService.ShowDialog(viewModel);
            if (result == true)
            {
                _mainViewModel.SwitchView(new ReserveTourViewModel(_mainViewModel, viewModel.InputedTouristNumber, tourInstance.Id, LoggedInUser, UserGiftCards, _dialogService));
            }
        }
    }
}
