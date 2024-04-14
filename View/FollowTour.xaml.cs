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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for FollowTour.xaml
    /// </summary>
    public partial class FollowTour : Window
    {
        public TourInstance TourInstance;
        public static ObservableCollection<TourInstance> tourInstances;

        public static ObservableCollection<KeyPoint> KeyPoints { get; set; }
        private readonly KeyPointRepository _keyPointRepository;


        public FollowTour(TourInstance tourInstance)
        {
            InitializeComponent();
            TourInstance = tourInstance;
            _keyPointRepository = new KeyPointRepository();
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointRepository.GetByTourInstance(TourInstance));

            KeyPointGrid.ItemsSource = KeyPoints;
            KeyPointGrid.DataContext = KeyPoints;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void EndTour_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTourists_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
