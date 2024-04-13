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
    /// Interaction logic for SpecificTourStatistics.xaml
    /// </summary>
    public partial class SpecificTourStatistics : Window
    {
        public User LoggedInUser { get; set; }
        public TourInstance TourInstance { get; set; }
        public TouristsStatistics TouristsStatistics { get; set; }

        public ObservableCollection<TouristsStatistics> TouristsStatisticsColl { get; set; }

        public TourReservation TourReservation { get; set; }
        private readonly TourReservationRepository _reservationRepository;
        public static ObservableCollection<TourReservation> TourReservations { get; set; }




        public SpecificTourStatistics(TourInstance tourInstance)
        {
            InitializeComponent();
            TourInstance = tourInstance;
            TouristsStatistics = new TouristsStatistics();
            TouristsStatistics.TourInstanceId = TourInstance.Id;
            TouristsStatisticsColl = new ObservableCollection<TouristsStatistics>();
            _reservationRepository = new TourReservationRepository();
            CountTouristsByAge();

            
        }


        private void CountTouristsByAge()
        {
            int Id = TouristsStatistics.TourInstanceId;
            //_reservationRepository = new TourReservationRepository();
            List<Tourist> tourists = new List<Tourist>(_reservationRepository.GetAllTouristByTourId(Id));
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age <= 18)
                {
                    TouristsStatistics.Young += 1;
                }
                else if(tourist.Age>18 && tourist.Age <= 50)
                {
                    TouristsStatistics.Middle += 1;
                }
                else if(tourist.Age > 50)
                {
                    TouristsStatistics.Old += 1;
                }
            }

            TouristsStatisticsColl.Add(TouristsStatistics);
            TouristsStatisticsGrid.ItemsSource = TouristsStatisticsColl;
            TouristsStatisticsGrid.DataContext = TouristsStatisticsColl;
        }


    }
}
