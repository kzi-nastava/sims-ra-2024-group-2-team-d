using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class ReserveTourViewModel: INotifyPropertyChanged
    {
        private ITouristRepository _touristRepository { get; set; }

        private ITourReservationRepository _tourReservationRepository { get; set; }

        private IGiftCardRepository _giftCardRepository { get; set; }

        private ITourInstanceService TourInstanceService { get; set; }

        public ObservableCollection<Tourist> Tourists { get; set; }

        public int TouristNumber { get; set; }

        public int TourInstanceId { get; set; }

        public User LoggedInUser { get; set; }

        public ObservableCollection<GiftCard> UserGiftCards { get; set; }

        public GiftCard SelectedGiftCard { get; set; }

        private int addedTouristsCounter;

        private bool isPlusButtonEnabled {  get; set; }
        public bool IsPlusButtonEnabled
        {
            get => isPlusButtonEnabled;
            set
            {
                if(value != isPlusButtonEnabled)
                {
                    isPlusButtonEnabled = value;
                    OnPropertyChanged(nameof(IsPlusButtonEnabled));
                }
            }
        }

        private bool isReserveButtonEnabled { get; set; }
        public bool IsReserveButtonEnabled
        {
            get => isReserveButtonEnabled;
            set
            {
                if (value != isReserveButtonEnabled)
                {
                    isReserveButtonEnabled = value;
                    OnPropertyChanged(nameof(IsReserveButtonEnabled));
                }
            }
        }

        private TouristDTO touristToAdd {  get; set; }
        public TouristDTO TouristToAdd
        {
            get => touristToAdd;
            set
            {
                if (value != touristToAdd)
                {
                    touristToAdd = value;
                    OnPropertyChanged(nameof(TouristToAdd));
                }
            }
        }
        public ICommand AddTouristCommand {  get; set; }
        public ICommand ReserveTourCommand {  get; set; }

        public ICommand BackButtonCommand {  get; set; }

        public int AddedTouristsCounter
        {
            get { return addedTouristsCounter; }
            set
            {
                addedTouristsCounter = value;
                OnPropertyChanged(nameof(AddedTouristsCounter));
            }
        }

        public TourInstance TourToReserve { get; set; }

        private readonly MainViewModel _mainViewModel;

        public string TourTitle {  get; set; }
        public DateTime ChosenDate {  get; set; }

        public IDialogService _dialogService {  get; set; }
        public ICommand DeleteTouristCommand { get; set; }
        public ICommand EditTouristCommand { get; set; }


        public ReserveTourViewModel(MainViewModel mainViewModel, int touristNumber, int tourInstanceId, User loggedInUser, ObservableCollection<GiftCard> userGiftCards, IDialogService dialogService)
        {
            _touristRepository = Injector.Injector.CreateInstance<ITouristRepository>();
            _tourReservationRepository = Injector.Injector.CreateInstance<ITourReservationRepository>();
            TourInstanceService = Injector.Injector.CreateInstance<ITourInstanceService>();
            _giftCardRepository = Injector.Injector.CreateInstance<IGiftCardRepository>();
            Tourists = new ObservableCollection<Tourist>();
            TourToReserve = TourInstanceService.GetById(tourInstanceId);
            TouristNumber = touristNumber;
            TourInstanceId = tourInstanceId;
            AddedTouristsCounter = TouristNumber;
            TourTitle = TourToReserve.BaseTour.Title;
            ChosenDate = TourToReserve.Date;
            LoggedInUser = loggedInUser;
            UserGiftCards = userGiftCards;
            AddTouristCommand = new RelayCommand(AddTourist);
            ReserveTourCommand = new RelayCommand(ReserveTour);
            _mainViewModel = mainViewModel;
            BackButtonCommand = new RelayCommand(GoBack);
            TouristToAdd = new TouristDTO();
            IsPlusButtonEnabled = true;
            IsReserveButtonEnabled = false;
            AddUserToList();
            _dialogService = dialogService;
            DeleteTouristCommand = new RelayCommand(tourist => DeleteTourist((Tourist)tourist));
            EditTouristCommand = new RelayCommand(tourist => EditTourist((Tourist)tourist));
        }

        public void EditTourist(Tourist tourist)
        {
            if(tourist.UserId == -1)
            {
                TouristToAdd = new TouristDTO(tourist);
                Tourists.Remove(tourist);
                AddedTouristsCounter++;
            }
            else
            {
                var feedbackViewModel = new FeedbackDialogViewModel("You can't edit yourself!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
            }          
        }

        public void DeleteTourist(Tourist tourist)
        {
            if(tourist.UserId == -1)
            {
                var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to delete this tourist?");
                bool? result = _dialogService.ShowDialog(confirmationViewModel);
                if (result == true)
                {                                         
                        Tourists.Remove(tourist);
                        AddedTouristsCounter++;                  
                }
            }
            else
            {
                var feedbackViewModel = new FeedbackDialogViewModel("You can't delete yourself!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
            }         
        }
        public void GoBack()
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to exit?");
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
            if(result == true)
            {
                _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, _dialogService));
            }
        }
        private void AddUserToList()
        {
            Tourist tourist = new Tourist(LoggedInUser.FirstName, LoggedInUser.LastName, LoggedInUser.Age, LoggedInUser.Id);
            Tourists.Add(tourist);
            if (--AddedTouristsCounter == 0)
            {
                IsPlusButtonEnabled = false;
                IsReserveButtonEnabled = true;
            }
        }

        private void AddTourist()
        {
            if (string.IsNullOrWhiteSpace(TouristToAdd?.Name) ||
                              string.IsNullOrWhiteSpace(TouristToAdd?.LastName) ||
                              TouristToAdd?.Age <= 0)
            {
                var feedbackViewModel = new FeedbackDialogViewModel("All fields for tourist information are required!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
                return;
            }
            Tourist tourist = new Tourist(TouristToAdd.Name, TouristToAdd.LastName, TouristToAdd.Age);
            Tourists.Add(tourist);
            if (--AddedTouristsCounter == 0)
            {
                IsPlusButtonEnabled = false;
                IsReserveButtonEnabled = true;
            }
            TouristToAdd.Name = "";
            TouristToAdd.LastName = "";
            TouristToAdd.Age = 0;
            TouristToAdd = new TouristDTO();
        }


        private void ReserveTour()
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to make a reservation?");
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
            if(result == true)
            {
                var feedbackViewModel = new FeedbackDialogViewModel("Tour reservation has been created!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
                TourReservation tourReservation = new TourReservation(TourInstanceId, LoggedInUser.Id);
                tourReservation = _tourReservationRepository.Save(tourReservation);
                foreach (var tourist in Tourists)
                {
                    tourist.ReservationId = tourReservation.Id;
                    _touristRepository.Save(tourist);
                }
                if (SelectedGiftCard != null)
                {
                    SelectedGiftCard.IsValid = false;
                    _giftCardRepository.UpdateValidStatus(SelectedGiftCard);
                }
                TourInstance tourInstance = TourInstanceService.GetById(TourInstanceId);
                tourInstance.EmptySpots -= TouristNumber;
                TourInstanceService.UpdateFreeSpots(tourInstance);
                _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, new DialogService()));
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
