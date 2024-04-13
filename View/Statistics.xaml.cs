using BookingApp.Model;
using BookingApp.Repository;
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
        public User LoggedInUser { get; set; }
        public static ObservableCollection<TourInstance> TourInstances { get; set; }
        public ObservableCollection<TourInstance> tourYear { get;set; }

        public ObservableCollection<TourInstance> instances { get; set; }
        //public List<TourInstance> list { get; set; }
        public TourRepository _tourRepository { get; set; }
        public TourInstanceRepository _instanceRepository{ get; set; }
        public Statistics(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            _tourRepository =new TourRepository();
            instances=new ObservableCollection<TourInstance>();
            _instanceRepository=new TourInstanceRepository();
            TourInstances = new ObservableCollection<TourInstance>(_instanceRepository.GetAll());

           // list = _instanceRepository.GetAll();
            LinkTourInstancesWithTours();
            TourInstances = new ObservableCollection<TourInstance>(_instanceRepository.GetAllFinishedByUser(user, TourInstances));
            MostVisited();
            tourYear = new ObservableCollection<TourInstance>();
        }
        public void LinkTourInstancesWithTours()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = _tourRepository.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }

        public void MostVisited()
        {
            if(TourInstances.Count == 0)
            {
                return;
            }
            int max = TourInstances[0].BaseTour.MaxTourists - TourInstances[0].EmptySpots;
            int index = 0;

            for(int i=1;i<TourInstances.Count;i++)
            {
                if(TourInstances[i].BaseTour.MaxTourists - TourInstances[i].EmptySpots > max && TourInstances[i].End == true)
                {
                    max = TourInstances[i].BaseTour.MaxTourists - TourInstances[i].EmptySpots;
                    index = i;
                }
            }

            instances.Add(TourInstances[index]);
            statisticsGrid.ItemsSource = instances;
            statisticsGrid.DataContext=instances;

        }


        public void MostVisitedByYear(int year)
        {
            List<TourInstance> list= TourInstances.Where(r=>r.Date.Year == year).ToList();
            if (list.Count == 0)
            {
                return;
            }
            int max = list[0].BaseTour.MaxTourists - list[0].EmptySpots;
            int index = 0;

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].BaseTour.MaxTourists - list[i].EmptySpots > max )
                {
                    max = list[i].BaseTour.MaxTourists - list[i].EmptySpots;
                    index = i;
                }
            }

            tourYear.Add(list[index]);
            statisticsGridYear.ItemsSource = tourYear;
            statisticsGridYear.DataContext = tourYear;

        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchBUtton_Click(object sender, RoutedEventArgs e)
        {
            if(SearchTextBox.Text=="")
            {
                return;
            }
            tourYear.Clear();
            int year =Convert.ToInt32( SearchTextBox.Text);
            MostVisitedByYear(year);
        }
    }
}
