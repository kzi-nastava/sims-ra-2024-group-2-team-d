using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel.Guide
{
    public class RequestsStatisticsViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser { get; set; }
        public string Location { get; set; }
        public string Lang { get; set; }
        public MyCommand CreateInLocCommand { get; set; }
        public MyCommand CreateInLangCommand { get; set; }

        public RequestsStatisticsViewModel(User user) 
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            Location = MainService.TourRequestService.FindMostWantedLocInLastYear();
            Lang = MainService.TourRequestService.FindMostWantedLangInLastYear();
            CreateInLocCommand = new MyCommand(CreateInLoc);
            CreateInLangCommand = new MyCommand(CreateInLang);
        }

        private void CreateInLoc()
        {
            TourCreationNotification notification = new TourCreationNotification();
            notification.IsBasedOnLocation = true;
            CreateTour createTour = new CreateTour(LoggedInUser, Location, "",notification);
            createTour.Show();
        }

        private void CreateInLang()
        {
            TourCreationNotification notification = new TourCreationNotification();
            notification.IsBasedOnLanguage = true;
            CreateTour createTour = new CreateTour(LoggedInUser, "", Lang, notification);
            createTour.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
