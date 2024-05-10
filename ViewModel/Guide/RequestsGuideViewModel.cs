using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel.Guide
{
    public class RequestsGuideViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser { get; set; }

        private ObservableCollection<TourRequest> tourRequests;
        public ObservableCollection<TourRequest> TourRequests { get { return tourRequests; } set {

                this.tourRequests = value;
                OnPropertyChanged("TourRequests");
            } }
        private TourRequest TourRequest { get; set; }

        public TourRequest SelectedTourRequest
        {
            get
            {
                return this.TourRequest;
            }
            set
            {
                this.TourRequest = value;
                OnPropertyChanged("SelectedTourRequest");
            }
        }
        private string startDate;
        public string StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        private string endDate;
        public string EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }
        private string lang;
        public string Lang
        {
            get { return lang; }
            set
            {
                lang = value;
                OnPropertyChanged("Lang");
            }
        }
        private string num;
        public string Num
        {
            get { return num; }
            set
            {
                num = value;
                OnPropertyChanged("Num");
            }
        }
        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }
        public MyCommand AcceptCommand { get; set; }
        public MyCommand SearchByLocationCommand { get; set; }
        public MyCommand SearchByNumCommand { get; set; }
        public MyCommand SearchByLanguageCommand { get; set; }
        public MyCommand SearchByDateCommand { get; set; }

        public RequestsGuideViewModel(User user)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            TourRequests = new ObservableCollection<TourRequest>(MainService.TourRequestService.getByStatus());
            AcceptCommand = new MyCommand(Accept);
            SearchByLocationCommand = new MyCommand(SearchByLocation);
            SearchByNumCommand = new MyCommand(SearchByNum);
            SearchByLanguageCommand = new MyCommand(SearchByLanguage);
            SearchByDateCommand = new MyCommand(SearchByDate);
        }

        private void Accept()
        {
            if (SelectedTourRequest == null)
            {
                MessageBox.Show("Must select specific tour request");
                return;
            }
            AcceptReqGuide acceptReqGuide = new AcceptReqGuide(SelectedTourRequest, LoggedInUser);
            acceptReqGuide.ShowDialog();
            ObservableCollection<TourRequest> tourRequests = new ObservableCollection<TourRequest>(MainService.TourRequestService.getByStatus());
            TourRequests = tourRequests;
        }

        private void SearchByDate()
        {
            if (StartDate == "") return;
            if (EndDate == "") return;
            DateOnly Start = DateOnly.ParseExact(StartDate, "yyyy-MM-dd", null);
            DateOnly End = DateOnly.ParseExact(EndDate, "yyyy-MM-dd", null);
            TourRequests = new ObservableCollection<TourRequest>(MainService.TourRequestService.FilterByDateRange(Start, End));
            //RequestsGrid.ItemsSource = TourRequests;
            //RequestsGrid.DataContext = TourRequests;
        }

        private void SearchByLanguage()
        {
            if (Lang == "") return;
            string Language = Lang;
            TourRequests = new ObservableCollection<TourRequest>(MainService.TourRequestService.FilterByLanguage(Language));
            //RequestsGrid.ItemsSource = TourRequests;
            //RequestsGrid.DataContext = TourRequests;
        }

        private void SearchByNum()
        {
            if (Num == "") return;
            int numnum = Convert.ToInt32(Num);
            TourRequests = new ObservableCollection<TourRequest>(MainService.TourRequestService.FilterByNumOfTourists(numnum));
            //RequestsGrid.ItemsSource = TourRequests;
            //RequestsGrid.DataContext = TourRequests;
        }

        private void SearchByLocation()
        {
            if (Location == "") return;
            string Loc = Location;
            TourRequests = new ObservableCollection<TourRequest>(MainService.TourRequestService.FilterByLocation(Loc));
            //RequestsGrid.ItemsSource = TourRequests;
            //RequestsGrid.DataContext = TourRequests;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
