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
    /// Interaction logic for RecommendedAlternatives.xaml
    /// </summary>
    public partial class RecommendedAlternatives : Window
    {
        public ObservableCollection<TourInstance> TourInstances { get; set; }

        public TourInstance SelectedTour {  get; set; }

        public User LoggedInUser { get; set; }
        public RecommendedAlternatives(ObservableCollection<TourInstance> tourInstances, User loggedInUser)
        {
            InitializeComponent();
            DataContext = this;
            TourInstances = tourInstances;
            LoggedInUser = loggedInUser;

        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            NumberOfTouristInsertion numberOfTouristInsertion = new NumberOfTouristInsertion(SelectedTour, TourInstances, LoggedInUser);
            numberOfTouristInsertion.Show();
        }
    }
}
