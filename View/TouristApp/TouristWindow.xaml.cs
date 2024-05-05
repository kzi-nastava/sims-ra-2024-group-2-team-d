using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.View.TouristApp;
using BookingApp.WPF.Views;
using BookingApp.WPF;
using BookingApp.Services;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TouristWindow : Window
    {

        public static ObservableCollection<TourInstance> TourInstances { get; set; }
        public static ObservableCollection<TourInstance> ActiveTours { get; set; }
        public TourInstance SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
        public ObservableCollection<GiftCard> UserGiftCards { get; set; }

        private readonly TourRepository _tourRepository;

        private readonly TourInstanceRepository _tourInstanceRepository;

        private readonly PictureRepository _pictureRepository;

        private readonly KeyPointRepository _keyPointRepository;

        private readonly TourReservationRepository _tourReservationRepository;

        private readonly GiftCardRepository _giftCardRepository;

        private readonly TouristNotificationsRepository _touristNotificationsRepository;

        public ObservableCollection<string> UniqueLanguages { get; set; }

        public ObservableCollection<TouristNotifications> Notifications { get; set; }

        public TouristNotificationService TouristNotificationService { get; set; }

        public ICommand Reserve { get; set; }

        public ICommand MoreInfoCommand {  get; set; }

        public ICommand OpenMorePicturesCommand {  get; set; }

        public ICommand OpenNotificationCommand {  get; set; }

        public ICommand MarkAsReadCommand {  get; set; }


        public TouristWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();           
            _tourInstanceRepository = new TourInstanceRepository();
            TourInstances = new ObservableCollection<TourInstance>(_tourInstanceRepository.GetAll());
            ActiveTours = new ObservableCollection<TourInstance>();
            UserGiftCards = new ObservableCollection<GiftCard>();
            Notifications = new ObservableCollection<TouristNotifications>();
            SelectedTour = new TourInstance();
            _pictureRepository = new PictureRepository();
            _keyPointRepository = new KeyPointRepository();
            _giftCardRepository = new GiftCardRepository();
            _tourReservationRepository = new TourReservationRepository();
            _touristNotificationsRepository = new TouristNotificationsRepository();
            TouristNotificationService = new TouristNotificationService();
            Reserve = new RelayCommand(tourInstance => MakeReservation((TourInstance)tourInstance));
            MoreInfoCommand = new RelayCommand(tourInstance => ShowMoreInfo((TourInstance)tourInstance));
            OpenMorePicturesCommand = new RelayCommand(tourInstance => OpenMorePictures((TourInstance)tourInstance));
            OpenNotificationCommand = new RelayCommand(notification => OpenNotification((TouristNotifications)notification));
            MarkAsReadCommand = new RelayCommand(notification => MarkAsRead((TouristNotifications)notification));
            LinkEntities();
            MoveToActiveTours();
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

        public void MarkAsRead(TouristNotifications notification)
        {
            notification.IsRead = true;
            TouristNotificationService.ChangeIsReadStatus(notification);
        }

        public void LinkNotifications()
        {
            List<TouristNotifications> notifications = _touristNotificationsRepository.GetByUserId(LoggedInUser.Id);
            notifications.Reverse();
            foreach(var notification in notifications)
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
            ShowMorePicturesView showMorePicturesView = new ShowMorePicturesView(tourInstance);
            showMorePicturesView.Show();
        }

        public void LinkKeyPointsWithTourInstances()
        {
            foreach(TourInstance tourInstance in TourInstances)
            {
                tourInstance.BaseTour.KeyPoints = _keyPointRepository.GetByTourInstance(tourInstance);
            }
        }

        public void LinkGiftCardWithUser()
        {
            List<GiftCard> giftCards = _giftCardRepository.GetAll();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            foreach (GiftCard giftCard in giftCards)
            {
                if(giftCard.UserId == LoggedInUser.Id && giftCard.IsValid && giftCard.ExpirationDate > today)
                {
                    UserGiftCards.Add(giftCard);
                }
            }
        }

       public void MoveToActiveTours()
        {
            List<TourReservation> userReservations = _tourReservationRepository.GetByUserId(LoggedInUser.Id);
            foreach(TourReservation userReservation in userReservations)
            {
                TourInstance tourInstance = _tourInstanceRepository.GetById(userReservation.TourInstanceId);
                if(tourInstance.Start && !tourInstance.End)
                {
                    ActiveTours.Add(tourInstance);
                }
            }
        }

        public void LinkTourInstancesWithTours()
        {
            foreach(TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = _tourRepository.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }

        public void LinkPicturesWithTours()
        {
            foreach(var tourInstance in TourInstances)
            {
                List<string> pictures = _pictureRepository.GetByTourId(tourInstance.TourId);
                if (pictures.Count() != 0)
                {
                    tourInstance.BaseTour.Pictures = pictures;
                }
            }
        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            bool isValidNumber = int.TryParse(numberOfPeopleInput.Text, out int numberOfPeople);
            if (!isValidNumber) numberOfPeople = 0;

            var filteredTours = TourInstances
                .Where(t => (string.IsNullOrEmpty(locationInput.Text) || t.BaseTour.Location.Contains(locationInput.Text, StringComparison.OrdinalIgnoreCase)) &&                          
                            (string.IsNullOrEmpty(durationInput.Text) || t.BaseTour.Duration.ToString().Contains(durationInput.Text)) &&
                            (languageInput.SelectedItem == null || t.BaseTour.Language.Contains(languageInput.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase)) &&
                            t.BaseTour.MaxTourists >= numberOfPeople)
                .ToList();

            TourInstances.Clear();
            foreach (var tour in filteredTours)
            {
                TourInstances.Add(tour);
            }
        }

        private void MakeReservation(TourInstance tourInstance)
        {
            NumberOfTouristInsertion numberOfTouristInsertion = new NumberOfTouristInsertion(tourInstance, TourInstances, LoggedInUser, UserGiftCards);
            numberOfTouristInsertion.Show();

        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            TourInstances.Clear();
            List<TourInstance> tourInstances = _tourInstanceRepository.GetAll();
            foreach (var tour in tourInstances)
            {
                TourInstances.Add(tour);
            }
            LinkEntities();
            EmptyTextBoxes();
        }

        private void EmptyTextBoxes()
        {
            locationInput.Text = string.Empty;
            durationInput.Text = string.Empty;
            languageInput.Text = string.Empty;
            numberOfPeopleInput.Text = string.Empty;
        }

        private void MyTours_Click(object sender, RoutedEventArgs e)
        {
            UserToursView userToursView = new UserToursView(LoggedInUser, TourInstances);
            userToursView.Show();
        }

        private void UserIcon_Click(object sender, RoutedEventArgs e)
        {
            menuPopup.IsOpen = !menuPopup.IsOpen;
        }

        private void ShowVouchers_Click(object sender, RoutedEventArgs e)
        {
            UserGiftCardView userGiftCardView = new UserGiftCardView(UserGiftCards);
            userGiftCardView.Show();
            menuPopup.IsOpen = false;
        }

        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            FollowingTourLiveView followingTourLiveView = new FollowingTourLiveView(SelectedTour);
            followingTourLiveView.Show();
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            notificationPopup.IsOpen = !notificationPopup.IsOpen;
            /*
            TouristNotificationView touristNotificationView = new TouristNotificationView(ActiveTours.ToList(), LoggedInUser);
            touristNotificationView.Show(); */
        }

        private void TypeOfTourRequestSelection_Click(object sender, RoutedEventArgs e)
        {
            TypeOfTourRequestSelectionView typeOfTourRequestSelectionView = new TypeOfTourRequestSelectionView();
            typeOfTourRequestSelectionView.ShowDialog();
        }

        public void ShowMoreInfo(TourInstance tourInstance)
        {
            MoreInfoAboutTourView moreInfoAboutTourView = new MoreInfoAboutTourView(tourInstance);
            moreInfoAboutTourView.Show();
        }

        public void OpenNotification(TouristNotifications notification)
        {
            if(notification.Type == NotificationType.AddedToLiveTour)
            {
                OpenLiveTourNotificationView liveTourNotification = new OpenLiveTourNotificationView(notification);
                liveTourNotification.Show();
                notificationPopup.IsOpen = !notificationPopup.IsOpen;
            }
        }
    }
}
