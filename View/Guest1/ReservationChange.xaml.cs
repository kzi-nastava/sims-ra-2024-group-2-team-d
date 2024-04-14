using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View.Guest1
{
    /// <summary>
    /// Interaction logic for ReservationChange.xaml
    /// </summary>
    public partial class ReservationChange : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ChangeReservationRequestRepository _changeReservationRequestRepository;
        private ReservationRepository _reservationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private int _userId;
        private int _ownerId;
        public ObservableCollection<KeyValuePair<int, string>> ReservationsForChange { get; set; }
        public int SelectedReservationId { get; set; }
        public DateTime NewCheckInDate { get; set; }
        public DateTime NewCheckOutDate { get; set; }
        private ObservableCollection<ChangeReservationRequest> _requests;
        public ObservableCollection<ChangeReservationRequest> Requests
        {
            get
            {
                return _requests;
            }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ReservationChange(int userId, ObservableCollection<ChangeReservationRequest> Requests)
        {
            InitializeComponent();
            DataContext = this;
            _reservationRepository = new ReservationRepository();
            _accommodationRepository = new AccommodationRepository();
            _changeReservationRequestRepository = new ChangeReservationRequestRepository();
            _userId = userId;
            this.Requests = Requests;
            InitializeReservationsForChange();
        }
        private void InitializeReservationsForChange()
        {
            ReservationsForChange = new ObservableCollection<KeyValuePair<int, string>>(_reservationRepository.GetReservationsByUserId(_userId));
        }

        private void SendRequest_Button(object sender, RoutedEventArgs e)
        {
            _ownerId = _accommodationRepository.getOwnerIdByAccommodationId(SelectedReservationId);
            string accommodationName = _accommodationRepository.getNameById(SelectedReservationId);
            //Reservation reservation = _reservationRepository.GetById(SelectedReservationId);
            ChangeReservationRequest request = new ChangeReservationRequest(SelectedReservationId, 1, accommodationName, NewCheckInDate, NewCheckOutDate, StatusType.Pending, _userId, _ownerId);
            _changeReservationRequestRepository.Save(request);
            Requests.Add(request);
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedReservationId != 0)
            {
                CheckInPicker.SelectedDate = _reservationRepository.GetCheckInDate(_userId, SelectedReservationId);
                CheckOutPicker.SelectedDate = _reservationRepository.GetCheckOutDate(_userId, SelectedReservationId);
            }
        }
    }
}
