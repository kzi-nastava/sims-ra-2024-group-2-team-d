using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel
{
    public class RequestsOverviewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ChangeReservationRequest> _requests;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly ChangeReservationRequestRepository _requestsRepository;
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


        public event PropertyChangedEventHandler? PropertyChanged;

        public RequestsOverviewViewModel(int userId)
        {
            _requestsRepository = new ChangeReservationRequestRepository();
            _accommodationRepository = new AccommodationRepository();
            Requests = new ObservableCollection<ChangeReservationRequest>(_requestsRepository.GetAllByUser(userId));
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}