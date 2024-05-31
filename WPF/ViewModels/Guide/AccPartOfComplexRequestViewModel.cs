using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class AccPartOfComplexRequestViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public TourRequest TourRequest { get; set; }
        public User LoggedInUser { get; set; }
        public ComplexTourRequest ComplexTourRequest { get; set; }
        public ObservableCollection<TourInstance> TourInstances { get; set; }
        //public ObservableCollection<DateTime> Slots { get; set; }
        public MyCommand OkCommand { get; set; }
        public MyCommand CancelCommand { get; set; }
        private DateTime selectedTime;
        public DateTime SelectedTime
        {
            get { return selectedTime; }
            set
            {
                selectedTime = value;
                OnPropertyChanged("SelectedTime");
            }
        }
        private ObservableCollection<DateTime> slots;
        public ObservableCollection<DateTime> Slots
        {
            get { return slots; }
            set
            {
                slots = value;
                OnPropertyChanged(nameof(Slots));
            }
        }
        public bool Check { get; set; }

        public AccPartOfComplexRequestViewModel(TourRequest tourRequest, User user, ComplexTourRequest complexTourRequest, Action closeAction)
        {
            MainService = MainService.GetInstance();
            Check = false;
            TourRequest = tourRequest;
            LoggedInUser = user;
            ComplexTourRequest = complexTourRequest;
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetAll());
            LinkTourInstancesWithTours();
            Slots = new ObservableCollection<DateTime>();
            TimeOnly timeOnly = new TimeOnly(0, 0);
            TimeOnly endTime = new TimeOnly(23, 59);
            Slots = GenerateHourlySlots(TourRequest.Start.ToDateTime(timeOnly), TourRequest.End.ToDateTime(endTime), GenerateExcludedSlots());
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

        private void Ok()
        {
            if (SelectedTime == null)
            {
                MessageBox.Show("You have to select date!");
            }
            else
            {
                TourRequest.CurrentStatus = Status.Accepted;
                TourRequest.ChosenDateTime = SelectedTime;
                TourRequest.GuideId = LoggedInUser.Id;
                MainService.TourRequestService.Update(TourRequest);
                MessageBoxResult result = MessageBox.Show("You have succesfully accepted this part of complex tour request!", "OK", MessageBoxButton.OK, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    Check = true;
                }
            }
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

        public ObservableCollection<DateTime> GenerateHourlySlots(DateTime startDate, DateTime endDate, List<DateTime> excludedSlots)
        {
            ObservableCollection<DateTime> hourlySlots = new ObservableCollection<DateTime>();
            DateTime current = startDate;
            while (current <= endDate)
            {
                if (!excludedSlots.Contains(current))
                {
                    hourlySlots.Add(current);
                }
                current = current.AddHours(1);
            }
            return hourlySlots;
        }

        public List<DateTime> GenerateExcludedSlots()
        {
            List<DateTime> excludedSlots = new List<DateTime>();
            excludedSlots = MainService.TourInstanceService.FindAllUnavaliableDates(LoggedInUser, TourInstances);
            excludedSlots.AddRange(MainService.TourRequestService.FindAllUnavaliableDates(LoggedInUser));
            excludedSlots.AddRange(MainService.ComplexTourRequestService.FindUnavaliableDates(ComplexTourRequest));
            return excludedSlots;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
