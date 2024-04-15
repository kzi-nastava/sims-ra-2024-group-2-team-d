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
        private readonly TourInstanceRepository _tourInstanceRepository;

        public static ObservableCollection<KeyPoint> KeyPoints { get; set; }
        private readonly KeyPointRepository _keyPointRepository;

        public static ObservableCollection<Tourist> Tourists { get; set; }
        private readonly TouristRepository _touristRepository;
        private readonly TourReservationRepository _tourReservationRepository;

        public FollowingTourLive FollowingTourLive;
        private readonly FollowingTourLiveRepository _followingTourLiveRepository;

        public Tourist SelectedTourist { get; set; }
        //public int i = 0;


        public FollowTour(TourInstance tourInstance)
        {
            InitializeComponent();
            TourInstance = tourInstance;
            TourInstance.Start = true;
            _tourInstanceRepository = new TourInstanceRepository();
            _tourInstanceRepository.Update(TourInstance);

            _keyPointRepository = new KeyPointRepository();
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointRepository.GetByTourInstance(TourInstance));
            KeyPoints[0].Status = true;
            FollowingTourLive = new FollowingTourLive();
            FollowingTourLive.TourInstanceId = TourInstance.Id;
            FollowingTourLive.KeyPointId = KeyPoints[0].Id;
            FollowingTourLiveRepository _followingTourLiveRepository = new FollowingTourLiveRepository();
            _followingTourLiveRepository.Save(FollowingTourLive);

            _tourReservationRepository = new TourReservationRepository();
            _touristRepository = new TouristRepository();
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
            KeyPoint keyPoint = checkBox.DataContext as KeyPoint;

            if (keyPoint == null)
                return;
           
            keyPoint.Status = true;

            FollowingTourLive = new FollowingTourLive();
            FollowingTourLive.TourInstanceId = TourInstance.Id;
            //int a = ++i;
            //KeyPoint keyPoint = FindLastlyChecked();
            //KeyPoints[a].Status = true;
            FollowingTourLive.KeyPointId = keyPoint.Id;

            FollowingTourLiveRepository _followingTourLiveRepository = new FollowingTourLiveRepository();
            _followingTourLiveRepository.Save(FollowingTourLive);

            if (AreAllCheckBoxesChecked())
            {
                EndTour.IsEnabled = true;
                EndInEmTour.IsEnabled = false;
            }
            else
            {
                EndTour.IsEnabled = false;
                EndInEmTour.IsEnabled = true;
            }
        }

        public bool AreAllCheckBoxesChecked()
        {           
            foreach (var item in KeyPointGrid.ItemsSource)
            {                
                var row = KeyPointGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;              
                if (row == null)
                    continue;
                var checkBoxCell = KeyPointGrid.Columns[1].GetCellContent(row) as CheckBox;               
                if (checkBoxCell == null || checkBoxCell.IsChecked != true)
                    return false;
            }
            return true;
        }

        public KeyPoint FindLastCheckedKeyPoint()
        {
            KeyPoint lastCheckedKeyPoint = null;

            foreach (var item in KeyPointGrid.ItemsSource)
            {
                var row = KeyPointGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;              
                if (row == null)
                    continue;
               
                var checkBoxCell = KeyPointGrid.Columns[1].GetCellContent(row) as CheckBox;              
                if (checkBoxCell == null || checkBoxCell.IsChecked != true)
                    continue;
               
                KeyPoint keyPoint = row.Item as KeyPoint;            
                lastCheckedKeyPoint = keyPoint;
            }
            return lastCheckedKeyPoint;
        }

        /*public KeyPoint FindLastlyChecked()
        {
            int MaxOrder = KeyPoints[0].Order;
            KeyPoint KeyPoint = KeyPoints[0];
            foreach (KeyPoint keyPoint in KeyPoints)
            {
                if (keyPoint.Status == true && keyPoint.Order > MaxOrder)
                {
                    MaxOrder = keyPoint.Order;
                    KeyPoint = keyPoint;
                }
            }
            return KeyPoint;
        }*/

        private void EndTour_Click(object sender, RoutedEventArgs e)
        {
            TourInstance.End = true;
            _tourInstanceRepository.Update(TourInstance);
            UserRepository _userRepository = new UserRepository();
            GuideWindow ft = new GuideWindow(_userRepository.GetById(TourInstance.BaseTour.UserId));
            ft.Show();
            this.Close();
        }

        private void AddTourists_Click(object sender, RoutedEventArgs e)
        {
            Tourist ti = TouristsGrid.SelectedItem as Tourist;
            if (ti != null)
            {
                FollowingTourLiveRepository _followingTourLiveRepository = new FollowingTourLiveRepository();
                List<FollowingTourLive> toursLive = new List<FollowingTourLive>(_followingTourLiveRepository.GetByTourInstanceId(TourInstance.Id));
                //FollowingTourLive followingTourLive = new FollowingTourLive();
                FollowingTourLive followingTourLive = toursLive.Find(r => r.KeyPointId == FindLastCheckedKeyPoint().Id);

                ti.ShowedUp = true;
                _touristRepository.Update(ti);
                followingTourLive.TouristsIds.Add(ti.Id);
                _followingTourLiveRepository.Update(followingTourLive);
            }
        }

        private void TouristsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row == null)
                return;

            Tourist selectedTourist = row.Item as Tourist;

            if (selectedTourist == null)
                return;

            (DataContext as FollowTour).SelectedTourist = selectedTourist;

            AddTourists_Click(sender, e);
        }

        /*private void AddTourists_Click(object sender, RoutedEventArgs e)
        {
            // Pronalaženje poslednjeg označenog KeyPoint-a
            KeyPoint lastCheckedKeyPoint = FindLastCheckedKeyPoint();

            // Ako nije pronađen nijedan označen KeyPoint, izlazimo iz metode
            if (lastCheckedKeyPoint == null)
            {
                MessageBox.Show("Please select a key point before adding tourists.");
                return;
            }

            // Kreiranje instance FollowingTourLiveRepository
            FollowingTourLiveRepository _followingTourLiveRepository = new FollowingTourLiveRepository();

            // Pronalaženje FollowingTourLive objekta koji odgovara poslednjem označenom KeyPoint-u
            FollowingTourLive followingTourLive = _followingTourLiveRepository.GetByTourInstanceIdAndKeyPointId(TourInstance.Id, lastCheckedKeyPoint.Id);

            // Ako nije pronađen odgovarajući FollowingTourLive objekat, izlazimo iz metode
            if (followingTourLive == null)
            {
                MessageBox.Show("Error: FollowingTourLive object not found.");
                return;
            }

            // Ažuriranje liste turista za trenutni FollowingTourLive objekat
            if (SelectedTourist != null)
            {
                SelectedTourist.ShowedUp = true;
                followingTourLive.TouristsIds.Add(SelectedTourist.Id);
                _followingTourLiveRepository.Update(followingTourLive);
            }
            else
            {
                MessageBox.Show("Please select a tourist before adding.");
            }
        }*/

        private void EndInEmTour_Click(object sender, RoutedEventArgs e)
        {
            TourInstance.End = true;
            _tourInstanceRepository.Update(TourInstance);
            UserRepository _userRepository = new UserRepository();
            GuideWindow ft = new GuideWindow(_userRepository.GetById(TourInstance.BaseTour.UserId));
            ft.Show();
            this.Close();
        }
    }
}
