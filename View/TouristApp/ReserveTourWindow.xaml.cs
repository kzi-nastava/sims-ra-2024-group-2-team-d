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

        private GiftCardRepository _giftCardRepository { get; set; }

        private TourInstanceRepository _tourInstanceRepository { get; set; }

        public ObservableCollection<Tourist> Tourists { get; set; }

        public int TouristNumber {  get; set; }

        public int TourInstanceId {  get; set; }

        public int AddedTouristsCounter {  get; set; }

        public User LoggedInUser {  get; set; }

        public ObservableCollection<GiftCard> UserGiftCards {  get; set; }

        public GiftCard SelectedGiftCard {  get; set; }

        public ReserveTourWindow(int touristNumber, int tourInstanceId, User loggedInUser, ObservableCollection<GiftCard> userGiftCards)
        {
            InitializeComponent();
            DataContext = this;
            _touristRepository = new TouristRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourInstanceRepository = new TourInstanceRepository();
            _giftCardRepository = new GiftCardRepository();
            Tourists = new ObservableCollection<Tourist>();
            TouristNumber = touristNumber;
            TourInstanceId = tourInstanceId;
            AddedTouristsCounter = TouristNumber;
            LoggedInUser = loggedInUser;
            UserGiftCards = userGiftCards;
            AddUserToList();
        }

        private void AddUserToList()
        {
            Tourist tourist = new Tourist(LoggedInUser.FirstName, LoggedInUser.LastName, LoggedInUser.Age);
            Tourists.Add(tourist);
            if (ReduceAndCheckTouristCounter())
            {
                ReserveTour();
            }
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
            TourReservation tourReservation = new TourReservation(TourInstanceId,TouristNumber,LoggedInUser.Id);
            tourReservation = _tourReservationRepository.Save(tourReservation);
            foreach (var tourist in Tourists)
            {
                tourist.ReservationId = tourReservation.Id;
                _touristRepository.Save(tourist);
            }
            if(SelectedGiftCard != null)
            {
                _giftCardRepository.Delete(SelectedGiftCard.Id);
            }
            TourInstance tourInstance = _tourInstanceRepository.GetById(TourInstanceId);
            tourInstance.EmptySpots -= TouristNumber;
            _tourInstanceRepository.UpdateFreeSpots(tourInstance);
            this.Close();
        }
    }
}
