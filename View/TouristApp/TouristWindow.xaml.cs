using BookingApp.Model;
using BookingApp.Repository;
using System;
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
using BookingApp.View.TouristApp;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TouristWindow : Window
    {
        public static ObservableCollection<Tour> BaseTours { get; set; }

        public static ObservableCollection<TourInstance> TourInstances { get; set; }

        public TourInstance SelectedTour { get; set; }

        public User LoggedInUser { get; set; }

        private readonly TourRepository _tourRepository;

        private readonly TourInstanceRepository _tourInstanceRepository;

        public TouristWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();
            BaseTours = new ObservableCollection<Tour>(_tourRepository.GetAll());
            _tourInstanceRepository = new TourInstanceRepository();
            TourInstances = new ObservableCollection<TourInstance>(_tourInstanceRepository.GetAll());
            SelectedTour = new TourInstance();
            LinkTourInstancesWithTours();

        }

        public void LinkTourInstancesWithTours()
        {
            foreach(TourInstance tourInstance in TourInstances)
            {
                if(_tourRepository.GetById(tourInstance.TourId) != null)
                {
                    tourInstance.BaseTour = _tourRepository.GetById(tourInstance.TourId);
                }
            }
        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            bool isValidNumber = int.TryParse(txtNumberOfPeople.Text, out int numberOfPeople);
            if (!isValidNumber) numberOfPeople = 0;

            var filteredTours = _tourRepository.GetAll()
                .Where(t => (string.IsNullOrEmpty(txtLocation.Text) || t.Location.Contains(txtLocation.Text, StringComparison.OrdinalIgnoreCase)) &&                          
                            (string.IsNullOrEmpty(txtDuration.Text) || t.Duration.ToString().Contains(txtDuration.Text)) &&
                            (string.IsNullOrEmpty(txtLanguage.Text) || t.Language.Contains(txtLanguage.Text, StringComparison.OrdinalIgnoreCase)) &&
                            t.MaxTourists >= numberOfPeople)
                .ToList();

            BaseTours.Clear();
            foreach (var tour in filteredTours)
            {
                BaseTours.Add(tour);
            }
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            NumberOfTouristInsertion numberOfTouristInsertion = new NumberOfTouristInsertion(SelectedTour, TourInstances);
            numberOfTouristInsertion.Show();

        }
    }
}
