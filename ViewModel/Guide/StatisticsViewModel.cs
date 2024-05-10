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
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourInstance> Instances { get; set; }
        public static ObservableCollection<TourInstance> TourInstances { get; set; }
        public ObservableCollection<TourInstance> TourInstancesYear { get; set; }
        public MyCommand SearchCommand { get; set; }
        public MyCommand RequestsCommand { get; set; }
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public StatisticsViewModel(User user)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetAll());
            LinkTourInstancesWithTours();
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetAllFinishedByUser(user, TourInstances));
            Instances = new ObservableCollection<TourInstance>();
            TourInstancesYear = new ObservableCollection<TourInstance>();
            MostVisited1();
            SearchCommand = new MyCommand(Search);
            RequestsCommand = new MyCommand(Requests);
        }

        private void Search()
        {
            if (SearchText == "")
            {
                return;
            }
            TourInstancesYear.Clear();
            int year = Convert.ToInt32(SearchText);
            MostVisitedByYear(year);
        }

        private void Requests()
        {
            RequestsStatistics requestsStatistics = new RequestsStatistics(LoggedInUser);
            requestsStatistics.Show();
        }

        public void MostVisitedByYear(int year)
        {
            List<TourInstance> list = TourInstances.Where(r => r.Date.Year == year).ToList();
            if (list.Count == 0) return;
            int max = 0;
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                List<Model.FollowingTourLive> ToursLive = new List<Model.FollowingTourLive>();
                ToursLive = MainService.FollowingTourLiveService.GetByTourInstanceId(list[i].Id);
                //List<Tourist> touristsReservation = new List<Tourist>(_reservationRepository.GetAllTouristByTourId(TourInstances[i].Id));
                List<int> tourists = new List<int>();
                tourists = GetShowedUpTourists(ToursLive);
                if (tourists.Count > max)
                {
                    max = tourists.Count;
                    index = i;
                }
            }
            if (index != -1 && list[index] != null)
            {
                TourInstancesYear.Add(list[index]);
                //statisticsGridYear.ItemsSource = tourYear;
                //statisticsGridYear.DataContext = tourYear;
            }
            else return;
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

        public void MostVisited1()
        {
            if (TourInstances.Count == 0) return;
            int max = 0;
            int index = -1;
            for (int i = 0; i < TourInstances.Count; i++)
            {
                List<Model.FollowingTourLive> ToursLive = new List<Model.FollowingTourLive>();
                ToursLive = MainService.FollowingTourLiveService.GetByTourInstanceId(TourInstances[i].Id);
                //List<Tourist> touristsReservation = new List<Tourist>(_reservationRepository.GetAllTouristByTourId(TourInstances[i].Id));
                List<int> tourists = new List<int>();
                tourists = GetShowedUpTourists(ToursLive);
                if (tourists.Count > max)
                {
                    max = tourists.Count;
                    index = i;
                }
            }
            if (index != -1 && TourInstances[index] != null)
            {
                Instances.Add(TourInstances[index]);
                //statisticsGrid.ItemsSource = instances;
                //statisticsGrid.DataContext = instances;
            }
            else return;
        }

        public List<int> GetShowedUpTourists(List<Model.FollowingTourLive> followingTourLive)
        {
            List<int> tourists = new List<int>();
            foreach (var item in followingTourLive)
            {
                foreach (var item1 in item.TouristsIds)
                {
                    tourists.Add(item1);
                }
            }
            return tourists;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
