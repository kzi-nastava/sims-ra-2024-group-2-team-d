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

namespace BookingApp.WPF.ViewModels.Guide
{
    public class RequestsLanguageStatisticsViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public string Text { get; set; }
        private string searchLangYear;
        public string SearchLangYear
        {
            get { return searchLangYear; }
            set
            {
                searchLangYear = value;
                OnPropertyChanged("SearchLangYear");
            }
        }
        private string langTotal;
        public string LangTotal
        {
            get { return langTotal; }
            set
            {
                langTotal = value;
                OnPropertyChanged("LangTotal");
            }
        }
        private string langYear;
        public string LangYear
        {
            get { return langYear; }
            set
            {
                langYear = value;
                OnPropertyChanged("LangYear");
            }
        }
        private Visibility isVisibleLangTotal;
        public Visibility IsVisibleLangTotal
        {
            get { return isVisibleLangTotal; }
            set
            {
                isVisibleLangTotal = value;
                OnPropertyChanged("IsVisibleLangTotal");
            }
        }
        private Visibility isVisibleLangYear;
        public Visibility IsVisibleLangYear
        {
            get { return isVisibleLangYear; }
            set
            {
                isVisibleLangYear = value;
                OnPropertyChanged("IsVisibleLangYear");
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
        public MyCommand SearchLangYearCommand { get; set; }

        public RequestsLanguageStatisticsViewModel(string text) 
        {
            MainService = MainService.GetInstance();
            Text = text;
            LangTotal = MainService.TourRequestService.CountRequestsOnLang(Text).ToString();
            IsVisibleLangTotal = Visibility.Visible;
            IsVisibleLangYear = Visibility.Hidden;
            SearchLangYearCommand = new MyCommand(SearchByLangAndYear);
            TourRequestsMonthStatisticsColl = new ObservableCollection<TourRequestsMonthStatistics>();
        }

        private void SearchByLangAndYear()
        {
            if (SearchLangYear == "") return;
            LangYear = MainService.TourRequestService.CountRequestsOnLangByYear(Text, Convert.ToInt32(SearchLangYear)).ToString();
            TourRequestsMonthStatistics TourRequestsMonthStatistics = new TourRequestsMonthStatistics(MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 1),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 2),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 3),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 4),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 5),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 6),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 7),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 8),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 9),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 10),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 11),
                                                                                                      MainService.TourRequestService.CountRequestsOnLangByYearAndMonth(Text, Convert.ToInt32(SearchLangYear), 12)
                                                                                                       );
            TourRequestsMonthStatisticsColl = new ObservableCollection<TourRequestsMonthStatistics>();
            TourRequestsMonthStatisticsColl.Add(TourRequestsMonthStatistics);
            IsVisibleLangYear = Visibility.Visible;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
