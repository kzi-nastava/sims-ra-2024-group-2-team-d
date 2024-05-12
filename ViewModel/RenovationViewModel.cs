using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.View.Owner;
using BookingApp.WPF.ViewModels.Guide;
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
    public class RenovationViewModel
    {
        private readonly AccommodationRepository _accommodationRepository;
        private readonly RenovationRepository _renovationRepository;

        private readonly User _user;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public string AccommodationName { get; set; }
        public string AccommodationCountry { get; set; }
        public string AccommodationCity { get; set; }

        public MyCommand onAccept { get; set; }

        public int NumberOfGuests { get; set; }
        private string _strNumberOfGuests;
        public string StrNumberOfGuests
        {
            get => _strNumberOfGuests;
            set
            {
                if (value != _strNumberOfGuests)
                {
                    try
                    {
                        int _numberOfGuests;
                        int.TryParse(value, out _numberOfGuests);
                        NumberOfGuests = _numberOfGuests;
                    }
                    catch (Exception) { }
                    _strNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ReservationDays { get; set; }
        private string _strReservationDays;
        public string StrReservationDays
        {
            get => _strReservationDays;
            set
            {
                if (value != _strReservationDays)
                {
                    try
                    {
                        int _reservationDays;
                        int.TryParse(value, out _reservationDays);
                        ReservationDays = _reservationDays;
                    }
                    catch (Exception) { }
                    _strReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isAppartmentSelected;
        public bool IsAppartmentSelected
        {
            get => _isAppartmentSelected;
            set
            {
                if (_isAppartmentSelected != value)
                {
                    _isAppartmentSelected = value;
                    OnPropertyChanged(nameof(IsAppartmentSelected));
                }
            }
        }
        private bool _isHouseSelected;
        public bool IsHouseSelected
        {
            get => _isHouseSelected;
            set
            {
                if (_isHouseSelected != value)
                {
                    _isHouseSelected = value;
                    OnPropertyChanged(nameof(IsHouseSelected));
                }
            }
        }
        private bool _isShackSelected;
        public bool IsShackSelected
        {
            get => _isShackSelected;
            set
            {
                if (_isShackSelected != value)
                {
                    _isShackSelected = value;
                    OnPropertyChanged(nameof(IsShackSelected));
                }
            }
        }

        public RenovationViewModel(User user)
        {
            
            
            _user = user;
            _accommodationRepository = new AccommodationRepository();
            _renovationRepository = new RenovationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            onAccept = new MyCommand(Reservation_Click);
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Reservation_Click()
        {
            SelectedAccommodation.Renovations = _renovationRepository.GetAll().Where(a => a.AccomodationId == SelectedAccommodation.Id).ToList();
            AccommodationRenovation accomodationRenovation = new AccommodationRenovation(_user, SelectedAccommodation);
            accomodationRenovation.Show();
        }
    }
}

