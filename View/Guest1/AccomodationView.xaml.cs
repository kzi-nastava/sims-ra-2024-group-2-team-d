using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.Owner;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace BookingApp.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccomodationView.xaml
    /// </summary>
    public partial class AccomodationView : Window, INotifyPropertyChanged
    {
        private readonly AccommodationRepository _accommodationRepository;
        private readonly ReservationRepository _reservationRepository;

        private readonly User _user;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public string AccommodationName { get; set; }
        public string AccommodationCountry { get; set; }
        public string AccommodationCity { get; set; }

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

        public AccomodationView(User user)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _accommodationRepository = new AccommodationRepository();
            _reservationRepository = new ReservationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool AccommodationNameFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(AccommodationName) || accommodation.Name.ToLower().Contains(AccommodationName.ToLower());
        }
        bool CountryFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(AccommodationCountry) || accommodation.Location.Country.ToLower().Contains(AccommodationCountry.ToLower());
        }
        bool CityFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(AccommodationCity) || accommodation.Location.City.ToLower().Contains(AccommodationCity.ToLower());
        }
        bool GuestNumberFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(StrNumberOfGuests) || accommodation.MaxGuestNumber >= NumberOfGuests;
        }
        bool DaysReservationFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(StrReservationDays) || accommodation.MinReservationDays <= ReservationDays;
        }
        private bool AccommodationTypeFilter(Accommodation accommodation)
        {
            if (IsAppartmentSelected && IsHouseSelected && IsShackSelected)
            {
                return accommodation.AccommodationType == AccommodationType.Apartman ||
                    accommodation.AccommodationType == AccommodationType.Kuca ||
                    accommodation.AccommodationType == AccommodationType.Koliba;
            }
            else if (IsAppartmentSelected && IsHouseSelected)
            {
                return accommodation.AccommodationType == AccommodationType.Apartman ||
                    accommodation.AccommodationType == AccommodationType.Kuca;
            }
            else if (IsAppartmentSelected && IsShackSelected)
            {
                return accommodation.AccommodationType == AccommodationType.Apartman ||
                    accommodation.AccommodationType == AccommodationType.Koliba;
            }
            else if (IsHouseSelected && IsShackSelected)
            {
                return accommodation.AccommodationType == AccommodationType.Kuca ||
                    accommodation.AccommodationType == AccommodationType.Koliba;
            }
            else if (IsAppartmentSelected)
            {
                return accommodation.AccommodationType == AccommodationType.Apartman;
            }
            else if (IsHouseSelected)
            {
                return accommodation.AccommodationType == AccommodationType.Kuca;
            }
            else if (IsShackSelected)
            {
                return accommodation.AccommodationType == AccommodationType.Koliba;
            }
            else
            {
                return true;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Accommodations);
            view.Filter = (obj) =>
            {
                Accommodation accommodation = obj as Accommodation;
                if (accommodation == null) return false;
                return (AccommodationNameFilter(accommodation) &&
                    CountryFilter(accommodation) &&
                    CityFilter(accommodation) &&
                    GuestNumberFilter(accommodation) &&
                    DaysReservationFilter(accommodation) &&
                    AccommodationTypeFilter(accommodation));
            };
        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodation.Reservations = _reservationRepository.GetAll().Where(a => a.AccomodationId == SelectedAccommodation.Id).ToList();
            AccomodationReservation accomodationReservation = new AccomodationReservation(_user, SelectedAccommodation);
            accomodationReservation.Show();
        }


    }
}
