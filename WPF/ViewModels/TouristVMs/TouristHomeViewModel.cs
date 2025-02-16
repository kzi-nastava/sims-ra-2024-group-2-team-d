﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.View.TouristApp;
using BookingApp.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using BookingApp.WPF.ViewModels.TouristVMs;
using BookingApp.Services.IServices;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class TouristHomeViewModel: INotifyPropertyChanged
    {
        public static ObservableCollection<TourInstance> TourInstances { get; set; }
        public static ObservableCollection<TourInstance> ActiveTours { get; set; }
        public TourInstance SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
        public ObservableCollection<GiftCard> UserGiftCards { get; set; }

        private readonly ITourService _tourService;

        private readonly ITourInstanceService _tourInstanceService;

        private readonly IUserService _userService;

        private readonly IPictureService _pictureService;

        private readonly IKeyPointService _keyPointService;

        private readonly ITourReservationService _tourReservationService;

        private readonly IGiftCardService _giftCardService;

        private readonly ITouristNotificationsService _touristNotificationsService;

        private ITourCreationNotificationService _tourCreationNotificationService;

        public ObservableCollection<string> UniqueLanguages { get; set; }

        private ObservableCollection<TouristNotifications> notifications;
        public ObservableCollection<TouristNotifications> Notifications
        {
            get => notifications;
            set
            {
                if(notifications != value)
                {
                    notifications = value;
                    OnPropertyChanged(nameof(Notifications));
                }
            }
        }

        public ICommand Reserve { get; set; }

        public ICommand MoreInfoCommand { get; set; }

        public ICommand OpenMorePicturesCommand { get; set; }

        public ICommand OpenNotificationCommand { get; set; }

        public ICommand MarkAsReadCommand { get; set; }

        public ICommand TrackTourLiveCommand { get; set; }

        public ICommand OpenTypeOfMyTourRequestSelectionCommand { get; set; }

        public ICommand OpenMyToursCommand { get; set; }

        public ICommand ShowNotificationsCommand {  get; set; }

        public ICommand UserIconClickCommand {  get; set; }

        public ICommand ShowToursitVouchersCommand {  get; set; }

        public ICommand ResetSearchResultsCommand { get; set; }

        public ICommand SearchCommand {  get; set; }

        public ICommand TypeOfTourRequestSelectionCommand { get; set; }

        public ICommand NavigateToReservationCommand { get; }

        private int numberOfPeopleSearch;
        public int NumberOfPeopleSearch
        {
            get => numberOfPeopleSearch;
            set
            {
                if (numberOfPeopleSearch != value)
                {
                    numberOfPeopleSearch = value;
                    OnPropertyChanged(nameof(NumberOfPeopleSearch));
                }
            }
        }

        public string locationSearch;
        public string LocationSearch
        {
            get => locationSearch;
            set
            {
                if (locationSearch != value)
                {
                    locationSearch = value;
                    OnPropertyChanged(nameof(LocationSearch));
                }
            }
        }

        private int durationSearch;
        public int DurationSearch
        {
            get => durationSearch;
            set
            {
                if (durationSearch != value)
                {
                    durationSearch = value;
                    OnPropertyChanged(nameof(DurationSearch));
                }
            }
        }

        private string languageSearch;
        public string LanguageSearch
        {
            get => languageSearch;
            set
            {
                if (languageSearch != value)
                {
                    languageSearch = value;
                    OnPropertyChanged(nameof(LanguageSearch));
                }
            }
        }

        private bool _isNotificationPopupOpen;
        public bool IsNotificationPopupOpen
        {
            get { return _isNotificationPopupOpen; }
            set
            {
                if (_isNotificationPopupOpen != value)
                {
                    _isNotificationPopupOpen = value;
                    OnPropertyChanged(nameof(IsNotificationPopupOpen));
                }
            }
        }

        private bool _isMenuPopupOpen;
        public bool IsMenuPopupOpen
        {
            get { return _isMenuPopupOpen; }
            set
            {
                if (_isMenuPopupOpen != value)
                {
                    _isMenuPopupOpen = value;
                    OnPropertyChanged(nameof(IsMenuPopupOpen));
                }
            }
        }

        public TourGiftCardAwardRecorderService TourGiftCardAwardRecorderService { get; set; }

        public bool HasNotifications
        {
            get { return Notifications.Count > 0; }
        }

        public bool HasActiveTours
        {
            get { return ActiveTours.Count > 0; }
        }

        private readonly MainViewModel _mainViewModel;

        private readonly IDialogService _dialogService;
        public ICommand PlayVideoCommand {  get; set; }

        public TouristHomeViewModel(MainViewModel mainViewModel, User user, IDialogService dialogService)
        {
            _mainViewModel = mainViewModel;
            LoggedInUser = user;
            _tourService = Injector.Injector.CreateInstance<ITourService>();
            _tourInstanceService = Injector.Injector.CreateInstance<ITourInstanceService>();
            _userService = Injector.Injector.CreateInstance<IUserService>();
            TourInstances = new ObservableCollection<TourInstance>(_tourInstanceService.GetAllForReservation());
            ActiveTours = new ObservableCollection<TourInstance>();
            UserGiftCards = new ObservableCollection<GiftCard>();
            Notifications = new ObservableCollection<TouristNotifications>();
            SelectedTour = new TourInstance();
            _pictureService = Injector.Injector.CreateInstance<IPictureService>();
            _keyPointService = Injector.Injector.CreateInstance<IKeyPointService>();
            _giftCardService = Injector.Injector.CreateInstance<IGiftCardService>();
            _tourReservationService = Injector.Injector.CreateInstance<ITourReservationService>();
            _touristNotificationsService = Injector.Injector.CreateInstance<ITouristNotificationsService>();
            //Reserve = new RelayCommand(tourInstance => MakeReservation((TourInstance)tourInstance));
            MoreInfoCommand = new RelayCommand(tourInstance => ShowMoreInfo((TourInstance)tourInstance));
            OpenMorePicturesCommand = new RelayCommand(tourInstance => OpenMorePictures((TourInstance)tourInstance));
            OpenNotificationCommand = new RelayCommand(notification => OpenNotification((TouristNotifications)notification));
            MarkAsReadCommand = new RelayCommand(notification => MarkAsRead((TouristNotifications)notification));
            TrackTourLiveCommand = new RelayCommand(tourInstance => TrackTourLive((TourInstance)tourInstance));
            OpenTypeOfMyTourRequestSelectionCommand = new RelayCommand(OpenTypeOfMyTourRequestSelection);
            OpenMyToursCommand = new RelayCommand(ShowTouristTours);
            ShowNotificationsCommand = new RelayCommand(ShowNotifications);
            UserIconClickCommand = new RelayCommand(UserIcon_Click);
            ShowToursitVouchersCommand = new RelayCommand(ShowTouristVouchers);
            ResetSearchResultsCommand = new RelayCommand(OnResetClick);
            SearchCommand = new RelayCommand(OnSearchClick);
            TypeOfTourRequestSelectionCommand = new RelayCommand(OpenTypeOfTourRequestSelection);
            NavigateToReservationCommand = new RelayCommand(tourInstance => ShowNumberOfTouristsDialog((TourInstance)tourInstance));
            _dialogService = dialogService;
            _tourCreationNotificationService = Injector.Injector.CreateInstance<ITourCreationNotificationService>();
            TourGiftCardAwardRecorderService = new TourGiftCardAwardRecorderService(Injector.Injector.CreateInstance<ITourGiftCardAwardRecorderRepository>(), Injector.Injector.CreateInstance<IGiftCardRepository>(), Injector.Injector.CreateInstance<ITourInstanceRepository>(), Injector.Injector.CreateInstance<ITouristRepository>());
            TourGiftCardAwardRecorderService.GiveGiftCardToEligibleTourist(user);
            LinkEntities();
            MoveToActiveTours();
            SortTourInstances(TourInstances);
            PlayVideoCommand = new RelayCommand(PlayVideo);
        }

        public void PlayVideo()
        {
            _mainViewModel.SwitchView(new VideoPlayerViewModel(_mainViewModel, LoggedInUser, _dialogService));
        }
        public void LinkEntities()
        {
            LinkTourInstancesWithTours();
            LinkKeyPointsWithTourInstances();
            LinkPicturesWithTours();
            LinkGiftCardWithUser();
            LoadUniqueLanguages();
            LinkNotifications();

        }

        public void OpenTypeOfMyTourRequestSelection()
        {
            //TypeOfMyTourRequestSelectionView view = new TypeOfMyTourRequestSelectionView(LoggedInUser);
            //view.Show();

            var viewModel = new TypeOfMyTourRequestSelectionViewModel(_mainViewModel,LoggedInUser);
            bool? result = _dialogService.ShowDialog(viewModel);
            if (result == true)
            {
                if (viewModel.SelectedOption == "Standard")
                {
                    _mainViewModel.SwitchView(new MyStandardTourRequestsViewModel(_mainViewModel, LoggedInUser,_dialogService));
                }
                else if (viewModel.SelectedOption == "Complex")
                {
                    _mainViewModel.SwitchView(new MyComplexTourRequestsViewModel(_mainViewModel, LoggedInUser, _dialogService));
                }
            }
        }

        public void MarkAsRead(TouristNotifications notification)
        {
            notification.IsRead = true;
            _touristNotificationsService.ChangeIsReadStatus(notification);
            Notifications.Remove(notification);
        }

        public void LinkNotifications()
        {
            List<TouristNotifications> notifications = _touristNotificationsService.GetByUserId(LoggedInUser.Id);
            notifications.Reverse();
            foreach (var notification in notifications)
            {
                Notifications.Add(notification);
            }
        }

        private void LoadUniqueLanguages()
        {
            UniqueLanguages = new ObservableCollection<string>(
                TourInstances.Select(t => t.BaseTour.Language).Distinct());
        }

        public void OpenMorePictures(TourInstance tourInstance)
        {
            var viewModel = new ShowMorePicturesViewModel(tourInstance);
            bool? result = _dialogService.ShowDialog(viewModel);
            //ShowMorePicturesView showMorePicturesView = new ShowMorePicturesView(tourInstance);
            //showMorePicturesView.Show();
        }

        public void LinkKeyPointsWithTourInstances()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                tourInstance.BaseTour.KeyPoints = _keyPointService.GetByTourInstance(tourInstance);
            }
        }

        public void LinkGiftCardWithUser()
        {
            List<GiftCard> giftCards = _giftCardService.GetAll();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            foreach (GiftCard giftCard in giftCards)
            {
                if (giftCard.UserId == LoggedInUser.Id && giftCard.IsValid && giftCard.ExpirationDate > today)
                {
                    UserGiftCards.Add(giftCard);
                }
            }
        }

        public void MoveToActiveTours()
        {
            List<TourReservation> userReservations = _tourReservationService.GetByUserId(LoggedInUser.Id);
            foreach (TourReservation userReservation in userReservations)
            {
                TourInstance tourInstance = _tourInstanceService.GetById(userReservation.TourInstanceId);
                if (tourInstance.Start && !tourInstance.End)
                {
                    ActiveTours.Add(tourInstance);
                }
            }
        }

        public void LinkTourInstancesWithTours()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = _tourService.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }

        public void LinkPicturesWithTours()
        {
            foreach (var tourInstance in TourInstances)
            {
                List<string> pictures = _pictureService.GetByTourId(tourInstance.TourId);
                if (pictures.Count() != 0)
                {
                    tourInstance.BaseTour.Pictures = pictures;
                }
            }
        }

        private void OnSearchClick()
        {
            //bool isValidNumber = int.TryParse(numberOfPeopleInput.Text, out int numberOfPeople);
            //if (!isValidNumber) numberOfPeople = 0;

            var filteredTours = TourInstances
                .Where(t => (string.IsNullOrEmpty(LocationSearch) || t.BaseTour.Location.Contains(LocationSearch, StringComparison.OrdinalIgnoreCase)) &&
                            (t.BaseTour.Duration == DurationSearch) &&
                            (LanguageSearch == null || t.BaseTour.Language.Contains(LanguageSearch, StringComparison.OrdinalIgnoreCase)) &&
                            t.BaseTour.MaxTourists >= NumberOfPeopleSearch)
                .ToList();

            TourInstances.Clear();
            foreach (var tour in filteredTours)
            {
                TourInstances.Add(tour);
            }
        }

        private void ShowNumberOfTouristsDialog(TourInstance tourInstance)
        {
                var viewModel = new NumberOfTouristInsertionViewModel(tourInstance, TourInstances, LoggedInUser, UserGiftCards, _dialogService);
                bool? result = _dialogService.ShowDialog(viewModel);
                if(result == true)
            {
                if(tourInstance.EmptySpots>=viewModel.InputedTouristNumber)
                {
                    _mainViewModel.SwitchView(new ReserveTourViewModel(_mainViewModel,viewModel.InputedTouristNumber, tourInstance.Id, LoggedInUser, UserGiftCards, _dialogService));
                }
                else if(tourInstance.EmptySpots == 0)
                {
                    _mainViewModel.SwitchView(new RecommendedAlternativeToursViewModel(_mainViewModel, viewModel.TourInstances, LoggedInUser, UserGiftCards, _dialogService));
                }
            }
            //NumberOfTouristInsertion numberOfTouristInsertion = new NumberOfTouristInsertion(tourInstance, TourInstances, LoggedInUser, UserGiftCards);
            //numberOfTouristInsertion.Show();

        }

        private void OnResetClick()
        {
            TourInstances.Clear();
            List<TourInstance> tourInstances = _tourInstanceService.GetAll();
            foreach (var tour in tourInstances)
            {
                TourInstances.Add(tour);
            }
            LinkEntities();
            EmptyTextBoxes();
            LocationSearch = new string("");
            DurationSearch = 0;
            LanguageSearch = new string("");
            NumberOfPeopleSearch = 0;
        }

        private void EmptyTextBoxes()
        {
            LocationSearch = string.Empty;
            DurationSearch = 0;
            LanguageSearch = string.Empty;
            NumberOfPeopleSearch = 0;
        }

        private void ShowTouristTours()
        {
            _mainViewModel.SwitchView(new UserToursViewModel(LoggedInUser, TourInstances, _dialogService, _mainViewModel));
        }

        private void UserIcon_Click()
        {
            IsMenuPopupOpen = !IsMenuPopupOpen;
        }

        private void ShowTouristVouchers()
        {
            //UserGiftCardView userGiftCardView = new UserGiftCardView(UserGiftCards);
            //userGiftCardView.Show();
            var viewModel = new UserGiftCardViewModel(UserGiftCards);
            bool? result = _dialogService.ShowDialog(viewModel);
            IsMenuPopupOpen = false;
        }

        private void TrackTourLive(TourInstance tourInstance)
        {
            //FollowingTourLiveView followingTourLiveView = new FollowingTourLiveView(tourInstance);
            //followingTourLiveView.Show();
            var viewModel = new FollowingTourLiveViewModel(tourInstance);
            bool? result = _dialogService.ShowDialog(viewModel);
        }

        private void ShowNotifications()
        {
            IsNotificationPopupOpen = !IsNotificationPopupOpen;
            /*
            TouristNotificationView touristNotificationView = new TouristNotificationView(ActiveTours.ToList(), LoggedInUser);
            touristNotificationView.Show(); */
        }

        private void OpenTypeOfTourRequestSelection()
        {
            var viewModel = new TypeOfTourRequestSelectionViewModel(LoggedInUser);
            bool? result = _dialogService.ShowDialog(viewModel);
            if (result == true)
            {
                if (viewModel.SelectedOption == "Standard")
                {
                    _mainViewModel.SwitchView(new CreateTourRequestViewModel(_mainViewModel, LoggedInUser, _dialogService));
                }
                else if(viewModel.SelectedOption == "Complex")
                {
                    _mainViewModel.SwitchView(new CreateComplexTourRequestViewModel(_mainViewModel, LoggedInUser,_dialogService));
                }
            }
        }

        public void ShowMoreInfo(TourInstance tourInstance)
        {
            var viewModel = new MoreInfoAboutTourViewModel(tourInstance, _dialogService);
            bool? result = _dialogService.ShowDialog(viewModel);
        }

        public void OpenNotification(TouristNotifications notification)
        {
            if (notification.Type == NotificationType.AddedToLiveTour)
            {
                var viewModel = new OpenLiveTourNotificationViewModel(notification);
                bool? result = _dialogService.ShowDialog(viewModel);
                //OpenLiveTourNotificationView liveTourNotification = new OpenLiveTourNotificationView(notification);
                //liveTourNotification.Show();
                IsNotificationPopupOpen = !IsNotificationPopupOpen;
            }
            else if (notification.Type == NotificationType.TourRequestAcceptance)
            {
                //MyStandardTourRequestsView view = new MyStandardTourRequestsView(LoggedInUser);
                //view.Show();
            }
            else if (notification.Type == NotificationType.TourCreation)
            {
                TourCreationNotification tourCreationNotification = _tourCreationNotificationService.GetById(notification.NotificationId);
                var service = Injector.Injector.CreateInstance<ITourInstanceService>();
                var viewModel = new MoreInfoAboutTourViewModel(service.GetById(tourCreationNotification.CreatedTourInstanceId),_dialogService);
                bool? result = _dialogService.ShowDialog(viewModel);
                //MoreInfoAboutTourView view = new MoreInfoAboutTourView(service.GetById(tourCreationNotification.CreatedTourInstanceId));
                //view.Show();
                IsNotificationPopupOpen = !IsNotificationPopupOpen;
            }
        }

        public bool CheckIfGuidedBySuperGuide(int id)
        {
            
            TourInstance instance = _tourInstanceService.GetById(id);
            if(_userService.GetAllSuperGuides(TourInstances.ToList()).Find(c=> c.Id == instance.BaseTour.UserId) == null)
            {
                return false;
            }
            else { return true; } 
        }

        public void SortTourInstances(ObservableCollection<TourInstance> tourInstances)
        {
            var sortedList = tourInstances.OrderByDescending(ti => CheckIfGuidedBySuperGuide(ti.Id)).ToList();
            tourInstances.Clear();
            foreach (var instance in sortedList)
            {
                tourInstances.Add(instance);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
