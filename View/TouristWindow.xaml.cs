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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TouristWindow : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }

        public Tour SelectedTour { get; set; }

        public User LoggedInUser { get; set; }

        private readonly TourRepository _repository;

        public TouristWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_repository.GetAll());
        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            bool isValidNumber = int.TryParse(txtNumberOfPeople.Text, out int numberOfPeople);
            if (!isValidNumber) numberOfPeople = 0;

            var filteredTours = _repository.GetAll()
                .Where(t => (string.IsNullOrEmpty(txtLocation.Text) || t.Location.Contains(txtLocation.Text, StringComparison.OrdinalIgnoreCase)) &&                          
                            (string.IsNullOrEmpty(txtDuration.Text) || t.Duration.ToString().Contains(txtDuration.Text)) &&
                            (string.IsNullOrEmpty(txtLanguage.Text) || t.Language.Contains(txtLanguage.Text, StringComparison.OrdinalIgnoreCase)) &&
                            t.MaxTourists >= numberOfPeople)
                .ToList();

            Tours.Clear();
            foreach (var tour in filteredTours)
            {
                Tours.Add(tour);
            }
        }
    }
}
