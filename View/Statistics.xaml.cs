using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.ViewModel.Guide;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        //public User LoggedInUser { get; set; }
        //public static ObservableCollection<TourInstance> TourInstances { get; set; }
        //public ObservableCollection<TourInstance> tourYear { get; set; }

        //public ObservableCollection<TourInstance> instances { get; set; }
        ////public List<TourInstance> list { get; set; }
        //public TourRepository _tourRepository { get; set; }
        //public TourInstanceRepository _instanceRepository { get; set; }
        //private readonly TourReservationRepository _reservationRepository;
        //public FollowingTourLiveRepository _followingTourLiveRepository { get; set; }

        public Statistics(User user)
        {
            InitializeComponent();
            DataContext = new StatisticsViewModel(user);
            //LoggedInUser = user;
            //_reservationRepository = new TourReservationRepository();
            //_followingTourLiveRepository = new FollowingTourLiveRepository();

            //_tourRepository = new TourRepository();
            //instances = new ObservableCollection<TourInstance>();
            //_instanceRepository = new TourInstanceRepository();
            //TourInstances = new ObservableCollection<TourInstance>(_instanceRepository.GetAll());

            //// list = _instanceRepository.GetAll();
            //LinkTourInstancesWithTours();
            //TourInstances = new ObservableCollection<TourInstance>(_instanceRepository.GetAllFinishedByUser(user, TourInstances));
            //MostVisited1();
            //tourYear = new ObservableCollection<TourInstance>();
        }
        //public void LinkTourInstancesWithTours()
        //{
        //    foreach (TourInstance tourInstance in TourInstances)
        //    {
        //        Tour baseTour = _tourRepository.GetById(tourInstance.TourId);
        //        if (baseTour != null)
        //        {
        //            tourInstance.BaseTour = baseTour;
        //        }
        //    }
        //}

        ///* public void MostVisited()
        // {
        //     if(TourInstances.Count == 0)
        //     {
        //         return;
        //     }
        //     int max = TourInstances[0].BaseTour.MaxTourists - TourInstances[0].EmptySpots;
        //     int index = 0;

        //     for(int i=1;i<TourInstances.Count;i++)
        //     {
        //         if(TourInstances[i].BaseTour.MaxTourists - TourInstances[i].EmptySpots > max && TourInstances[i].End == true)
        //         {
        //             max = TourInstances[i].BaseTour.MaxTourists - TourInstances[i].EmptySpots;
        //             index = i;
        //         }
        //     }

        //     instances.Add(TourInstances[index]);
        //     statisticsGrid.ItemsSource = instances;
        //     statisticsGrid.DataContext=instances;

        // }*/

        //public void MostVisited1()
        //{
        //    if (TourInstances.Count == 0) return;
        //    int max = 0;
        //    int index = -1;
        //    for (int i = 0; i < TourInstances.Count; i++)
        //    {
        //        List<FollowingTourLive> ToursLive = new List<FollowingTourLive>();
        //        ToursLive = _followingTourLiveRepository.GetByTourInstanceId(TourInstances[i].Id);
        //        //List<Tourist> touristsReservation = new List<Tourist>(_reservationRepository.GetAllTouristByTourId(TourInstances[i].Id));
        //        List<int> tourists = new List<int>();
        //        tourists = GetShowedUpTourists(ToursLive);
        //        if (tourists.Count > max)
        //        {
        //            max = tourists.Count;
        //            index = i;
        //        }
        //    }
        //    if (index != -1 && TourInstances[index] != null)
        //    {
        //        instances.Add(TourInstances[index]);
        //        statisticsGrid.ItemsSource = instances;
        //        statisticsGrid.DataContext = instances;
        //    }
        //    else return;
        //}

        //public List<int> GetShowedUpTourists(List<FollowingTourLive> followingTourLive)
        //{
        //    List<int> tourists = new List<int>();
        //    foreach (var item in followingTourLive)
        //    {
        //        foreach (var item1 in item.TouristsIds)
        //        {
        //            tourists.Add(item1);
        //        }
        //    }
        //    return tourists;
        //}

        //public void MostVisitedByYear(int year)
        //{
        //    List<TourInstance> list = TourInstances.Where(r => r.Date.Year == year).ToList();
        //    if (list.Count == 0) return;
        //    int max = 0;
        //    int index = -1;
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        List<Model.FollowingTourLive> ToursLive = new List<Model.FollowingTourLive>();
        //        ToursLive = _followingTourLiveRepository.GetByTourInstanceId(list[i].Id);
        //        //List<Tourist> touristsReservation = new List<Tourist>(_reservationRepository.GetAllTouristByTourId(TourInstances[i].Id));
        //        List<int> tourists = new List<int>();
        //        tourists = GetShowedUpTourists(ToursLive);
        //        if (tourists.Count > max)
        //        {
        //            max = tourists.Count;
        //            index = i;
        //        }
        //    }
        //    if (index != -1 && list[index] != null)
        //    {
        //        tourYear.Add(list[index]);
        //        statisticsGridYear.ItemsSource = tourYear;
        //        statisticsGridYear.DataContext = tourYear;
        //    }
        //    else return;
        //}


        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //}

        //private void SearchBUtton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (SearchTextBox.Text == "")
        //    {
        //        return;
        //    }
        //    tourYear.Clear();
        //    int year = Convert.ToInt32(SearchTextBox.Text);
        //    MostVisitedByYear(year);
        //}

        //private void Requests_Click(object sender, RoutedEventArgs e)
        //{
        //    RequestsStatistics requestsStatistics = new RequestsStatistics(LoggedInUser);
        //    requestsStatistics.Show();
        //}
    }
}