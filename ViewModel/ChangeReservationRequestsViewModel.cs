using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.Owner;
using BookingApp.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel
{
    public class ChangeReservationRequestsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ChangeReservationRequestRepository _changeReservationRequestRepository;

        public ObservableCollection<ChangeReservationRequest> _requests;

        private readonly ReservationRepository _reservationRepository;
        private readonly AccommodationRepository _accommodationRepository;

        public ChangeReservationRequest SelectedRequest { get; set; }

        public ObservableCollection<ChangeReservationRequest> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }


        public ChangeReservationRequestsViewModel(int userId)
        {
            _changeReservationRequestRepository = new ChangeReservationRequestRepository();
            _reservationRepository = new ReservationRepository();
            _accommodationRepository = new AccommodationRepository();
            var accommodations = _accommodationRepository.GetAllOwnerAccommodations(userId);
            var idList = accommodations.Select(obj => obj.Id).ToList();
            Requests = new ObservableCollection<ChangeReservationRequest>(_changeReservationRequestRepository.GetAllPendingReservationRequests(idList));
        }





        public void Accept_Click()
        {
            if (SelectedRequest == null)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                _changeReservationRequestRepository.AcceptRequest(SelectedRequest.RequestId);
                _reservationRepository.ChangeReservationDateRange(SelectedRequest.NewStartDate, SelectedRequest.NewEndDate, SelectedRequest.ReservationId);
                Requests.Remove(SelectedRequest);
            }
        }

        public void Decline_Click()
        {
            if (SelectedRequest == null)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                DeclineRequest declineRequest = new DeclineRequest(SelectedRequest.RequestId);
                declineRequest.Show();

            }
        }





        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
