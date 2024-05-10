using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel.Guide
{
    public class AcceptReqGuideViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public TourRequest TourRequest { get; set; }
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourInstance> TourInstances { get; set; }
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

        public AcceptReqGuideViewModel(TourRequest tourRequest, User user)
        {
            MainService = MainService.GetInstance();
            TourRequest = tourRequest;
            LoggedInUser = user;
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetAll());
            LinkTourInstancesWithTours();
            OkCommand = new MyCommand(Ok);
            CancelCommand = new MyCommand(Cancel);
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
                        MainService.TourRequestService.Update(TourRequest);
                        //CreateTourInstanceFromRequest(TourRequest, parsedDate);

                        MessageBox.Show("You have successfully acceptted this request!");
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
