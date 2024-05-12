using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View;
using BookingApp.WPF.Views.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class RequestsStatisticsViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser { get; set; }
        public string Location { get; set; }
        public string Lang { get; set; }
        public MyCommand CreateInLocCommand { get; set; }
        public MyCommand CreateInLangCommand { get; set; }
        private string searchLocText;
        public string SearchLocText
        {
            get { return searchLocText; }
            set
            {
                searchLocText = value;
                OnPropertyChanged("SearchLocText");
            }
        }
        private string searchLocYear;
        public string SearchLocYear
        {
            get { return searchLocYear; }
            set
            {
                searchLocYear = value;
                OnPropertyChanged("SearchLocYear");
            }
        }
        private string locTotal;
        public string LocTotal
        {
            get { return locTotal; }
            set
            {
                locTotal = value;
                OnPropertyChanged("LocTotal");
            }
        }
        private string locYear;
        public string LocYear
        {
            get { return locYear; }
            set
            {
                locYear = value;
                OnPropertyChanged("LocYear");
            }
        }
        private Visibility isVisibleLocTotal;
        public Visibility IsVisibleLocTotal
        {
            get { return isVisibleLocTotal; }
            set
            {
                isVisibleLocTotal = value;
                OnPropertyChanged("IsVisibleLocTotal");
            }
        }
        private Visibility isVisibleLocYear;
        public Visibility IsVisibleLocYear
        {
            get { return isVisibleLocYear; }
            set
            {
                isVisibleLocYear = value;
                OnPropertyChanged("IsVisibleLocYear");
            }
        }
        private ObservableCollection<TourRequestsMonthStatistics> tourRequestsMonthStatisticsColl;
        public ObservableCollection<TourRequestsMonthStatistics> TourRequestsMonthStatisticsColl
        {
            get { return tourRequestsMonthStatisticsColl; }
            set
            {

                tourRequestsMonthStatisticsColl = value;
                OnPropertyChanged("TourRequestsMonthStatisticsColl");
            }
        }
        public MyCommand SearchLocCommand { get; set; }
        public MyCommand SearchLocYearCommand { get; set; }
        public MyCommand SearchLangCommand { get; set; }

        public RequestsStatisticsViewModel(User user)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            Location = MainService.TourRequestService.FindMostWantedLocInLastYear();
            Lang = MainService.TourRequestService.FindMostWantedLangInLastYear();
            CreateInLocCommand = new MyCommand(CreateInLoc);
            CreateInLangCommand = new MyCommand(CreateInLang);
            IsVisibleLocTotal = Visibility.Hidden;
            IsVisibleLocYear = Visibility.Hidden;
            SearchLocCommand = new MyCommand(SearchByLoc);
            SearchLangCommand = new MyCommand(SearchByLang);
            SearchLocYearCommand = new MyCommand(SearchByLocAndYear);
            TourRequestsMonthStatisticsColl = new ObservableCollection<TourRequestsMonthStatistics>();
        }

        private void SearchByLang()
        {
            if (SearchLocText == "") return;
            RequestsLanguageStatistics requestsLanguageStatistics = new RequestsLanguageStatistics(SearchLocText);
            requestsLanguageStatistics.Show();
        }

        private void SearchByLoc()
        {
            if (SearchLocText == "") return;
            LocTotal = MainService.TourRequestService.CountRequestsOnLoc(SearchLocText).ToString();
            IsVisibleLocTotal = Visibility.Visible;
            IsVisibleLocYear = Visibility.Hidden;
        }

        private void SearchByLocAndYear()
        {
            if (SearchLocYear == "") return;
            LocYear = MainService.TourRequestService.CountRequestsOnLocByYear(SearchLocText, Convert.ToInt32(SearchLocYear)).ToString();
            TourRequestsMonthStatistics TourRequestsMonthStatistics = new TourRequestsMonthStatistics(MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 1),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 2),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 3),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 4),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 5),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 6),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 7),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 8),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 9),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 10),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 11),
                                                                                                      MainService.TourRequestService.CountRequestsOnLocByYearAndMonth(SearchLocText, Convert.ToInt32(SearchLocYear), 12)
                                                                                                       );
            TourRequestsMonthStatisticsColl = new ObservableCollection<TourRequestsMonthStatistics>();
            TourRequestsMonthStatisticsColl.Add(TourRequestsMonthStatistics);
            IsVisibleLocYear = Visibility.Visible;
        }



        private void CreateInLoc()
        {
            TourCreationNotification notification = new TourCreationNotification();
            notification.IsBasedOnLocation = true;
            CreateTour createTour = new CreateTour(LoggedInUser, Location, "", notification);
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
