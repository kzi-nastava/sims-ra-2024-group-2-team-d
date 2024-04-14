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

        public static ObservableCollection<Tourist> Tourists { get; set; }
        private readonly TouristRepository _touristRepository;
        private readonly TourReservationRepository _tourReservationRepository;

        public FollowingTourLive FollowingTourLive;
        private readonly FollowingTourLiveRepository _followingTourLiveRepository;

        public Tourist SelectedTourist { get; set; }
        


        public FollowTour(TourInstance tourInstance)
        {
            InitializeComponent();
            TourInstance = tourInstance;
            TourInstance.Start = true;
            
            _keyPointRepository = new KeyPointRepository();
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointRepository.GetByTourInstance(TourInstance));
            KeyPoints[0].Status = true;

            _tourReservationRepository = new TourReservationRepository();
            Tourists = new ObservableCollection<Tourist>(_tourReservationRepository.GetAllTouristByTourId(TourInstance.Id));

            KeyPointGrid.ItemsSource = KeyPoints;
            KeyPointGrid.DataContext = KeyPoints;

            TouristsGrid.ItemsSource = Tourists;
            TouristsGrid.DataContext = Tourists;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == null)
                return;

            KeyPoint checkedKeyPoint = checkBox.DataContext as KeyPoint;
            if (checkedKeyPoint == null)
                return;

            int index = KeyPoints.IndexOf(checkedKeyPoint);

            bool allPreviousMarked = true;
            for (int i = 0; i < index; i++)
            {
                if (!KeyPoints[i].Status)
                {
                    allPreviousMarked = false;
                    break;
                }
            }

            if (!allPreviousMarked)
            {
                MessageBox.Show("You can not skip Key Points!.");
                checkBox.IsChecked = false;
                return;
            }

            FollowingTourLive = new FollowingTourLive();
            FollowingTourLive.TourInstanceId = TourInstance.Id;

            KeyPoint keyPoint = FindLastlyChecked();

            FollowingTourLive.KeyPointId = keyPoint.Id;

            FollowingTourLiveRepository _followingTourLiveRepository = new FollowingTourLiveRepository();
            _followingTourLiveRepository.Save(FollowingTourLive);

        }

        public KeyPoint FindLastlyChecked()
        {
            int MaxOrder = KeyPoints[0].Order;
            KeyPoint KeyPoint = new KeyPoint();
            foreach (KeyPoint keyPoint in KeyPoints)
            {
                if (keyPoint.Status == true && keyPoint.Order > MaxOrder)
                {
                    MaxOrder = keyPoint.Order;
                    KeyPoint = keyPoint;
                }
            }
            return KeyPoint;
        }

        private void EndTour_Click(object sender, RoutedEventArgs e)
        {


        }

        private void AddTourists_Click(object sender, RoutedEventArgs e)
        {
            FollowingTourLiveRepository _followingTourLiveRepository = new FollowingTourLiveRepository();
            List<FollowingTourLive> toursLive = new List<FollowingTourLive>(_followingTourLiveRepository.GetByTourInstanceId(TourInstance.Id));
            //FollowingTourLive followingTourLive = new FollowingTourLive();
            FollowingTourLive followingTourLive = toursLive.Find(r => r.KeyPointId == FindLastlyChecked().Id);
            if(SelectedTourist != null)
            {
                followingTourLive.TouristsIds.Add(SelectedTourist.Id);
                _followingTourLiveRepository.Update(followingTourLive);
                var touristsToRemove = Tourists.Where(r => r.Id == SelectedTourist.Id).ToList();
                foreach (var tourist in touristsToRemove)
                {
                    Tourists.Remove(tourist);
                }
                
            }
        }

        private void EndInEmTour_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
