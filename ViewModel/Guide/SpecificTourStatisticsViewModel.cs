using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.Guide
{
    public class SpecificTourStatisticsViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public TourInstance TourInstance { get; set; }
        public TouristsStatistics TouristsStatistics { get; set; }
        public ObservableCollection<TouristsStatistics> TouristsStatisticsColl { get; set; }

        public SpecificTourStatisticsViewModel(TourInstance tourInstance)
        {
            MainService = MainService.GetInstance();
            TourInstance = tourInstance;
            TouristsStatistics = new TouristsStatistics();
            TouristsStatistics.TourInstanceId = TourInstance.Id;
            TouristsStatisticsColl = new ObservableCollection<TouristsStatistics>();
            CountTouristsByAge();
        }

        private void CountTouristsByAge()
        {
            int Id = TouristsStatistics.TourInstanceId;
            //_reservationRepository = new TourReservationRepository();
            List<FollowingTourLive> ToursLive = new List<FollowingTourLive>();
            ToursLive = MainService.FollowingTourLiveService.GetByTourInstanceId(Id);
            List<Tourist> touristsReservation = new List<Tourist>(MainService.TourReservationService.GetAllTouristByTourId(Id));
            List<Tourist> tourists = new List<Tourist>();
            tourists = GetTourists(touristsReservation, ToursLive);
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age <= 18)
                {
                    TouristsStatistics.Young += 1;
                }
                else if (tourist.Age > 18 && tourist.Age <= 50)
                {
                    TouristsStatistics.Middle += 1;
                }
                else if (tourist.Age > 50)
                {
                    TouristsStatistics.Old += 1;
                }
            }
            TouristsStatisticsColl.Add(TouristsStatistics);
            //TouristsStatisticsGrid.ItemsSource = TouristsStatisticsColl;
            //TouristsStatisticsGrid.DataContext = TouristsStatisticsColl;
        }

        public List<Tourist> GetTourists(List<Tourist> tourists, List<FollowingTourLive> followingToursLive)
        {
            List<Tourist> final = new List<Tourist>();
            foreach (var item in tourists)
            {
                foreach (FollowingTourLive item1 in followingToursLive)
                {
                    foreach (var item2 in item1.TouristsIds)
                    {
                        if (item.Id == item2 && item1.TourInstanceId == MainService.TourReservationService.GetTourInstanceById(item.ReservationId) && !final.Contains(item))
                        {
                            final.Add(item);
                        }
                    }
                }
            }
            return final;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
