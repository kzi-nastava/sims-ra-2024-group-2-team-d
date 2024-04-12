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

        public static ObservableCollection<TourInstance> TourInstances { get; set; }

        public static ObservableCollection<TourInstance> ActiveTours { get; set; }

        public TourInstance SelectedTour { get; set; }

        public User LoggedInUser { get; set; }

        private readonly TourRepository _tourRepository;

        private readonly TourInstanceRepository _tourInstanceRepository;

        private readonly PictureRepository _pictureRepository;

        private readonly KeyPointRepository _keyPointRepository;


        public TouristWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();           
            _tourInstanceRepository = new TourInstanceRepository();
            TourInstances = new ObservableCollection<TourInstance>(_tourInstanceRepository.GetAll());
            ActiveTours = new ObservableCollection<TourInstance>();
            SelectedTour = new TourInstance();
            _pictureRepository = new PictureRepository();
            _keyPointRepository = new KeyPointRepository();
            LinkEntities();
            MoveToActiveTours();


        }

        public void LinkEntities()
        {
            LinkTourInstancesWithTours();
            LinkPicturesWithTours();

        }

       public void MoveToActiveTours()
        {
            foreach(TourInstance tourInstance in TourInstances)
            {
                if (tourInstance.Start && !tourInstance.End){
                    ActiveTours.Add(tourInstance);
                    //TourInstances.Remove(tourInstance);
                }
            }
        }

        public void LinkTourInstancesWithTours()
        {
            foreach(TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = _tourRepository.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }

        public void LinkPicturesWithTours()
        {
            foreach(var tourInstance in TourInstances)
            {
                List<string> pictures = _pictureRepository.GetByTourId(tourInstance.TourId);
                if (pictures.Count() != 0)
                {
                    tourInstance.BaseTour.Pictures = pictures;
                }
            }
        }


        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            bool isValidNumber = int.TryParse(numberOfPeopleInput.Text, out int numberOfPeople);
            if (!isValidNumber) numberOfPeople = 0;

            var filteredTours = TourInstances
                .Where(t => (string.IsNullOrEmpty(locationInput.Text) || t.BaseTour.Location.Contains(locationInput.Text, StringComparison.OrdinalIgnoreCase)) &&                          
                            (string.IsNullOrEmpty(durationInput.Text) || t.BaseTour.Duration.ToString().Contains(durationInput.Text)) &&
                            (languageInput.SelectedItem == null || t.BaseTour.Language.Contains((languageInput.SelectedItem as dynamic).BaseTour.Language, StringComparison.OrdinalIgnoreCase)) &&
                            t.BaseTour.MaxTourists >= numberOfPeople)
                .ToList();

            TourInstances.Clear();
            foreach (var tour in filteredTours)
            {
                TourInstances.Add(tour);
            }
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            NumberOfTouristInsertion numberOfTouristInsertion = new NumberOfTouristInsertion(SelectedTour, TourInstances, LoggedInUser);
            numberOfTouristInsertion.Show();

        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            TourInstances.Clear();
            List<TourInstance> tourInstances = _tourInstanceRepository.GetAll();
            foreach (var tour in tourInstances)
            {
                TourInstances.Add(tour);
            }
            LinkEntities();
            EmptyTextBoxes();
        }

        private void EmptyTextBoxes()
        {
            locationInput.Text = string.Empty;
            durationInput.Text = string.Empty;
            languageInput.Text = string.Empty;
            numberOfPeopleInput.Text = string.Empty;
        }

       
    }
}
