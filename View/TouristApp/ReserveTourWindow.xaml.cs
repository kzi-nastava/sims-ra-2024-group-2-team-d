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
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.View.TouristApp
{
    /// <summary>
    /// Interaction logic for ReserveTourWindow.xaml
    /// </summary>
    public partial class ReserveTourWindow : Window
    {
        private TouristRepository _touristRepository {  get; set; }

        private TourReservationRepository _tourReservationRepository {  get; set; }

        public ObservableCollection<Tourist> Tourists { get; set; }

        private TourInstanceRepository _tourInstanceRepository { get; set; }

        public int TouristNumber {  get; set; }

        public int TourInstanceId {  get; set; }

        public int AddedTouristsCounter {  get; set; }
        public ReserveTourWindow(int touristNumber, int tourInstanceId)
        {
            InitializeComponent();
            DataContext = this;
            _touristRepository = new TouristRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourInstanceRepository = new TourInstanceRepository();
            Tourists = new ObservableCollection<Tourist>();
            TouristNumber = touristNumber;
            TourInstanceId = tourInstanceId;
            AddedTouristsCounter = TouristNumber;
        }

        private void AddTouristButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameInput.Text;
            string lastName = lastNameInput.Text;
            int age = int.Parse(ageInput.Text);
            Tourist tourist = new Tourist(name, lastName, age);
            Tourists.Add(tourist); 
            
            if (ReduceAndCheckTouristCounter())
            {
                ReserveTour();
            }
        }

        private bool ReduceAndCheckTouristCounter()
        {
            return --AddedTouristsCounter == 0;
        }

        private void ReserveTour()
        {
            TourReservation tourReservation = new TourReservation(TourInstanceId,TouristNumber);
            tourReservation = _tourReservationRepository.Save(tourReservation);
            foreach (var tourist in Tourists)
            {
                tourist.ReservationId = tourReservation.Id;
                _touristRepository.Save(tourist);
            }
            TourInstance tourInstance = _tourInstanceRepository.GetById(TourInstanceId);
            tourInstance.EmptySpots -= TouristNumber;
            _tourInstanceRepository.UpdateFreeSpots(tourInstance);
            this.Close();
        }
    }
}
