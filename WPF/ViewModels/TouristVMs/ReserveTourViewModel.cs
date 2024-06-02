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


        public ReserveTourViewModel(MainViewModel mainViewModel, int touristNumber, int tourInstanceId, User loggedInUser, ObservableCollection<GiftCard> userGiftCards)
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
        }
        public void GoBack()
        {
            _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, new DialogService()));
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
