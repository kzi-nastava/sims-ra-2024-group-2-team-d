using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class CreateTourViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }

        private TourCreationNotificationService _tourCreationNotificationService;

        private TourRequestService _tourRequestService;
        public User LoggedInUser { get; set; }
        public TourDto Tour { get; set; }
        public TourInstance TourInstance { get; set; }
        public KeyPoint KeyPoint { get; set; }
        public Picture Picture { get; set; }

        public TourCreationNotification NewTourCreationNotification { get; set; }
        public MyCommand CreateNewTourCommand { get; set; }
        public MyCommand CancelCommand { get; set; }
        private Visibility isVisibleError;
        public Visibility IsVisibleError
        {
            get { return isVisibleError; }
            set
            {
                isVisibleError = value;
                OnPropertyChanged("IsVisibleError");
            }
        }
        private string errorText;
        public string ErrorText
        {
            get { return errorText; }
            set
            {
                errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }
        //private string locText;
        //public string LocText
        //{
        //    get { return locText; }
        //    set
        //    {
        //        locText = value;
        //        OnPropertyChanged("LocText");
        //    }
        //}
        //private string langText;
        //public string LangText
        //{
        //    get { return langText; }
        //    set
        //    {
        //        langText = value;
        //        OnPropertyChanged("LangText");
        //    }
        //}
        private bool locReadOnly;
        public bool LocReadOnly
        {
            get { return locReadOnly; }
            set
            {
                locReadOnly = value;
                OnPropertyChanged("LocReadOnly");
            }
        }
        private bool langReadOnly;
        public bool LangReadOnly
        {
            get { return langReadOnly; }
            set
            {
                langReadOnly = value;
                OnPropertyChanged("LangReadOnly");
            }
        }
        public string Loc;
        public string Lang;

        public CreateTourViewModel(User user, string location, string lang)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            Tour = new TourDto();
            Tour.UserId = user.Id;
            TourInstance = new TourInstance();
            KeyPoint = new KeyPoint();
            Picture = new Picture();
            CreateNewTourCommand = new MyCommand(CreateNewTour);
            CancelCommand = new MyCommand(Cancel);
            IsVisibleError = Visibility.Hidden;
            Loc = location;
            Lang = lang;
            LocReadOnly = false;
            LangReadOnly = false;
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            CheckLoc();
            CheckLang();
        }

        public CreateTourViewModel(User user, string location, string lang, TourCreationNotification tourCreationNotification)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            Tour = new TourDto();
            Tour.UserId = user.Id;
            TourInstance = new TourInstance();
            KeyPoint = new KeyPoint();
            Picture = new Picture();
            CreateNewTourCommand = new MyCommand(CreateNewTour);
            CancelCommand = new MyCommand(Cancel);
            IsVisibleError = Visibility.Hidden;
            Loc = location;
            Lang = lang;
            LocReadOnly = false;
            LangReadOnly = false;
            _tourCreationNotificationService = new TourCreationNotificationService(Injector.Injector.CreateInstance<ITourCreationNotificationRepository>());
            NewTourCreationNotification = tourCreationNotification;
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            CheckLoc();
            CheckLang();
        }

        public void CheckLoc()
        {
            if (Loc != "")
            {
                Tour.Location = Loc;
                LocReadOnly = true;
            }
        }

        public void CheckLang()
        {
            if (Lang != "")
            {
                Tour.Language = Lang;
                LangReadOnly = true;
            }
        }

        private void CreateNewTour()
        {
            string validTour = Tour.IsValid;

            if (validTour == string.Empty)
            {
                Tour newTour = Tour.ToModel();
                Tour newT = MainService.TourService.JustSave(newTour);
                List<TourInstance> newTourInstances = new List<TourInstance>();
                newTourInstances = newT.TourInstances;
                foreach (TourInstance instance in newTourInstances)
                {
                    if (MainService.TourService.GetById(instance.TourId) != null)
                    {
                        instance.BaseTour = MainService.TourService.GetById(instance.TourId);
                    }

                    TourInstance savedTourInstance = MainService.TourInstanceService.Save(instance);
                    if (NewTourCreationNotification != null)
                    {
                        NotifyUsers(newT, savedTourInstance);
                    }
                }



                List<Picture> newPictures = new List<Picture>();
                newPictures = newT.ClassPictures;
                foreach (Picture picture in newPictures)
                {
                    MainService.PictureService.Save(picture);
                }

                List<KeyPoint> newKeyPoints = new List<KeyPoint>();
                newKeyPoints = newT.KeyPoints;
                foreach (KeyPoint keyPoint in newKeyPoints)
                {
                    MainService.KeyPointService.Save(keyPoint);
                }
                /* TourInstancesColl = new ObservableCollection<TourInstance>();

                 LinkTourInstancesWithTours();*/

                //this.Close();
            }
            else
            {
                IsVisibleError = Visibility.Visible;
                ErrorText = validTour;
            }
        }

        public void NotifyUsers(Tour savedTour, TourInstance savedTourInstance)
        {
            if (NewTourCreationNotification.IsBasedOnLanguage)
            {
                List<int> userIds = _tourRequestService.FindUserIdsByLanguage(savedTour.Language);
                foreach (int userId in userIds)
                {
                    NewTourCreationNotification.CreatedTourInstanceId = savedTourInstance.Id;
                    TourCreationNotification savedNotification = _tourCreationNotificationService.Save(NewTourCreationNotification);
                    TouristNotifications notification = new TouristNotifications(savedNotification.Id, "A tour based on a language you requested has been created. Click \"See more\" to see more info about tour", NotificationType.TourCreation, userId);
                    TouristNotificationsService _touristNotificationsService = new TouristNotificationsService(Injector.Injector.CreateInstance<ITouristNotificationsRepository>());
                    _touristNotificationsService.Save(notification);
                }
            }
            else if (NewTourCreationNotification.IsBasedOnLocation)
            {
                List<int> userIds = _tourRequestService.FindUserIdsByLocation(savedTour.Location);
                foreach (int userId in userIds)
                {
                    NewTourCreationNotification.CreatedTourInstanceId = savedTourInstance.Id;
                    TourCreationNotification savedNotification = _tourCreationNotificationService.Save(NewTourCreationNotification);
                    TouristNotifications notification = new TouristNotifications(savedNotification.Id, "A tour based on a location you requested has been created. Click \"See more\" to see more info about tour", NotificationType.TourCreation, userId);
                    TouristNotificationsService _touristNotificationsService = new TouristNotificationsService(Injector.Injector.CreateInstance<ITouristNotificationsRepository>());
                    _touristNotificationsService.Save(notification);
                }
            }

        }

        private void Cancel()
        {
            //Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
