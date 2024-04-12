using BookingApp.Model;
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

namespace BookingApp.View.TouristApp
{
    /// <summary>
    /// Interaction logic for NumberOfTouristInsertion.xaml
    /// </summary>
    public partial class NumberOfTouristInsertion : Window
    {
       
        public TourInstance SelectedTour { get; set; }

        public ObservableCollection<TourInstance> TourInstances {  get; set; }

        public User LoggedInUser { get; set; }
        public NumberOfTouristInsertion(TourInstance selectedTour, ObservableCollection<TourInstance> tourInstances, User loggedInUser)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;
            TourInstances = new ObservableCollection<TourInstance>();
            FilterToursDependingOnLocation(tourInstances);
            LoggedInUser = loggedInUser;
        }

        void FilterToursDependingOnLocation(ObservableCollection<TourInstance> tourInstances)
        {
            foreach(TourInstance tour in tourInstances)
            {
                if(tour.BaseTour.Location == SelectedTour.BaseTour.Location && tour.Id != SelectedTour.Id)
                {
                    TourInstances.Add(tour);
                }
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            int touristNumber = int.Parse(touristNumberInput.Text);
            if (SelectedTour.EmptySpots >= touristNumber)
            {
                ReserveTourWindow reserveTourWindow = new ReserveTourWindow(touristNumber, SelectedTour.Id, LoggedInUser);
                reserveTourWindow.Show();
                this.Close();
            }
            else if(SelectedTour.EmptySpots != 0 && SelectedTour.EmptySpots < touristNumber)
            {
                textBox.Text = string.Format("There is only {0} spots left. Please enter a fewer number of tourists or choose a different tour", SelectedTour.EmptySpots);
                textBox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                RecommendedAlternatives recommendedAlternatives = new RecommendedAlternatives(TourInstances, LoggedInUser);
                recommendedAlternatives.Show();
                this.Close();
            }
        }

        
    }
}
