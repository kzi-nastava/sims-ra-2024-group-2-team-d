using BookingApp.Domain.Model;
using BookingApp.View.TouristApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class NumberOfTouristInsertionViewModel: IRequestClose
    {
        public TourInstance SelectedTour { get; set; }

        public ObservableCollection<TourInstance> TourInstances { get; set; }

        public User LoggedInUser { get; set; }

        public ObservableCollection<GiftCard> UserGiftCards { get; set; }

        public int InputedTouristNumber {  get; set; }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }
        private readonly IDialogService _dialogService;


        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
        public NumberOfTouristInsertionViewModel(TourInstance selectedTour, ObservableCollection<TourInstance> tourInstances, User loggedInUser, ObservableCollection<GiftCard> userGiftCards, IDialogService dialogService)
        {
            SelectedTour = selectedTour;
            TourInstances = new ObservableCollection<TourInstance>();
            FilterToursDependingOnLocation(tourInstances);
            LoggedInUser = loggedInUser;
            UserGiftCards = userGiftCards;
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Close);
            _dialogService = dialogService;
        }

        void FilterToursDependingOnLocation(ObservableCollection<TourInstance> tourInstances)
        {
            foreach (TourInstance tour in tourInstances)
            {
                if (tour.BaseTour.Location == SelectedTour.BaseTour.Location && tour.Id != SelectedTour.Id)
                {
                    TourInstances.Add(tour);
                }
            }
        }


        private void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));
            //this.Close();
        }

        private void Confirm()
        {
            //int touristNumber = int.Parse(InputedTouristNumber);
            if (SelectedTour.EmptySpots >= InputedTouristNumber)
            {
               // ReserveTourWindow reserveTourWindow = new ReserveTourWindow(InputedTouristNumber, SelectedTour.Id, LoggedInUser, UserGiftCards);
                //reserveTourWindow.Show();
                RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));
                //this.Close();
            }
            else if (SelectedTour.EmptySpots != 0 && SelectedTour.EmptySpots < InputedTouristNumber)
            {
                //textBox.Text = string.Format("There is only {0} spots left. Please enter a fewer number of tourists or choose a different tour", SelectedTour.EmptySpots);
                //textBox.Foreground = new SolidColorBrush(Colors.Red);
                string message = string.Format("There are only {0} empty spots. Please insert lesser number of tourist of choose a different tour", SelectedTour.EmptySpots);
                var feedbackViewModel = new FeedbackDialogViewModel(message);
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
            }
            else
            {
                //RecommendedAlternatives recommendedAlternatives = new RecommendedAlternatives(TourInstances, LoggedInUser, UserGiftCards);
                //recommendedAlternatives.Show();
                //this.Close();
                RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));
            }
        }
    }
}
