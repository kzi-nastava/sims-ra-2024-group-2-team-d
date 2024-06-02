using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.Services.IServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class AcceptReqGuideViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public TourRequest TourRequest { get; set; }
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourInstance> TourInstances { get; set; }
        public ITourRequestAcceptanceNotificationService _tourRequestAcceptanceNotificationService { get; set; }
        public MyCommand OkCommand { get; set; }
        public MyCommand CancelCommand { get; set; }
        private string dateText;
        public string DateText
        {
            get { return dateText; }
            set
            {
                dateText = value;
                OnPropertyChanged("DateText");
            }
        }
        public bool Check { get; set; }

        public AcceptReqGuideViewModel(TourRequest tourRequest, User user, Action closeAction)
        {
            MainService = MainService.GetInstance();
            Check = false;
            TourRequest = tourRequest;
            LoggedInUser = user;
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetAll());
            _tourRequestAcceptanceNotificationService = Injector.Injector.CreateInstance<ITourRequestAcceptanceNotificationService>();
            LinkTourInstancesWithTours();
            OkCommand = new MyCommand(() =>
            {
                Ok();
                if (Check == true) { closeAction(); }
            });
            CancelCommand = new MyCommand(() =>
            {
                closeAction();
            });
        }

        public void LinkTourInstancesWithTours()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = MainService.TourService.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }

        private void Ok()
        {
            if (DateText == "")
            {
                MessageBox.Show("You have to set date!");
            }
            else
            {
                string format = "yyyy-MM-dd HH:mm";
                DateTime parsedDate;
                if (DateTime.TryParseExact(DateText, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    DateTime StartDateTime = new DateTime(TourRequest.Start.Year, TourRequest.Start.Month, TourRequest.Start.Day);
                    DateTime EndDateTime = new DateTime(TourRequest.End.Year, TourRequest.End.Month, TourRequest.End.Day);
                    if (parsedDate.Date >= StartDateTime && parsedDate.Date <= EndDateTime
                        && MainService.TourInstanceService.CheckIfUserIsAvaliable(LoggedInUser, parsedDate, TourInstances)
                        && MainService.TourRequestService.CheckUserIsAvaliable(LoggedInUser, parsedDate))
                    {
                        TourRequest.CurrentStatus = Status.Accepted;
                        TourRequest.ChosenDateTime = parsedDate;
                        TourRequest.GuideId = LoggedInUser.Id;
                        TourRequest tourRequest = MainService.TourRequestService.Update(TourRequest);
                        NotifyTouristUser(tourRequest);
                        //CreateTourInstanceFromRequest(TourRequest, parsedDate);
                        MessageBoxResult result = MessageBox.Show("You have succesfully accepted this request!", "OK", MessageBoxButton.OK, MessageBoxImage.Question);
                        if (result == MessageBoxResult.OK)
                        {
                            Check = true;
                        }
                        //this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You have to enter date in valid date range");
                    }
                }
                else
                {
                    MessageBox.Show("You have to enter date in format yyyy-MM-dd HH:mm");
                }
            }
        }

        private void NotifyTouristUser(TourRequest tourRequest)
        {
            TourRequestAcceptanceNotification tourRequestAcceptanceNotification = new TourRequestAcceptanceNotification(tourRequest.Id);
            TourRequestAcceptanceNotification savedNotification = _tourRequestAcceptanceNotificationService.Save(tourRequestAcceptanceNotification);
            TouristNotifications notification = new TouristNotifications(savedNotification.Id, "Your request has been accepted. Click \"See more\" to see more info", NotificationType.TourRequestAcceptance, tourRequest.UserTouristId);
            ITouristNotificationsService _touristNotificationsService = Injector.Injector.CreateInstance<ITouristNotificationsService>();
            _touristNotificationsService.Save(notification);

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
