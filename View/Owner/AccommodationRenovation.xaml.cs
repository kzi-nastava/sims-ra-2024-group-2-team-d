using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View.Guest1;
using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationRenovation.xaml
    /// </summary>
    public partial class AccommodationRenovation : Window, INotifyPropertyChanged
    {
        private ObservableCollection<DateRange> _dateRanges;
        private readonly RenovationRepository _renovationRepository;
        private readonly RenovationRepository _RenovationRepository;

        private BaseService BaseService { get; set; }

        private readonly User _user;
        public DateRange SelectedDateRange { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public string AccommodationName { get; set; }
        public List<Renovation> Renovations { get; set; }
        public ObservableCollection<DateRange> DateRanges
        {
            get { return _dateRanges; }
            set
            {
                _dateRanges = value;
                OnPropertyChanged(nameof(DateRanges));
            }
        }
        private DateTime _startDay;
        public DateTime StartDay
        {
            get
            {
                return _startDay;
            }
            set
            {
                if (value != _startDay)
                {
                    _startDay = value;
                    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, StartDay));
                    OnPropertyChanged("StartDay");
                }
            }
        }
        private DateTime _endDay;
        public DateTime EndDay
        {
            get
            {
                return _endDay;
            }
            set
            {
                if (value != _endDay)
                {
                    _endDay = value;
                    OnPropertyChanged("EndDay");
                }
            }
        }

        private int _reservationDays;
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
                    OnPropertyChanged(nameof(StrReservationDays));
                }
            }
        }
        private int _numberOfGuests;
        public int NumberOfGuests
        {
            get { return _numberOfGuests; }
            set
            {
                _numberOfGuests = value;
                OnPropertyChanged("NumberOfGuests");
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public AccommodationRenovation(User user, Accommodation accommodation)
        {
            InitializeComponent();
            this.DataContext = this;
            _user = user;
            SelectedAccommodation = accommodation;
            AccommodationName = accommodation.Name;
            Renovations = accommodation.Renovations ?? new List<Renovation>();
            _renovationRepository = new RenovationRepository();
            _RenovationRepository = new RenovationRepository();
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            DateRanges = new ObservableCollection<DateRange>();
            BaseService = BaseService.getInstance();

        }

        public event PropertyChangedEventHandler? PropertyChanged;


        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            NoFreeReservation.Visibility = Visibility.Hidden;
            if (ReservationDays < SelectedAccommodation.MinReservationDays)
            {
                MessageBox.Show("Minimalan broj nociju za rezervaciju " + AccommodationName + " je " + SelectedAccommodation.MinReservationDays);
            }
            else if (ReservationDays > (EndDay - StartDay).Days)
            {
                MessageBox.Show("Opseg datuma koji ste izabrali je kraci od izabranog broja dana");
            }
            else
            {
                DateRanges.Clear();
                ShowFreeDatesForReservation();
            }
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if(rezervacije.SelectedIndex==-1)
            {
                MessageBox.Show("Niste izabrali datum");
                return;
            }
            RenovationComment rc = new RenovationComment(rezervacije.SelectedItem as DateRange,SelectedAccommodation,_user);
            rc.Show();
        }

        private List<DateRange> ExtractFreeDates(DateTime StartDay, DateTime EndDay)
        {
            List<DateRange> allDates = GetAllPossibleDates(StartDay, EndDay);
            List<DateRange> datesToRemove = new List<DateRange>();
            foreach (Renovation renovation in Renovations)
            {
                foreach (DateRange range in allDates)
                {
                    if (renovation.RenovationDateRange.WithinRange(range) && !datesToRemove.Contains(range))
                    {
                        datesToRemove.Add(range);
                    }
                }
            }
            List<Reservation> Reservations = BaseService.ReservationService._repository.GetAll();
            foreach (Reservation reservation in Reservations)
            {
                foreach (DateRange range in allDates)
                {
                    if (reservation.ReservationDateRange.WithinRange(range) && !datesToRemove.Contains(range))
                    {
                        datesToRemove.Add(range);
                    }
                }
            }
            RemoveUnavailableDates(allDates, datesToRemove);
            return allDates;
        }
        private void RemoveUnavailableDates(List<DateRange> allDates, List<DateRange> datesToRemove)
        {
            foreach (DateRange range in datesToRemove)
            {
                DateRange dateRange = allDates.Find(r => r.StartDate == range.StartDate && r.EndDate == range.EndDate);
                allDates.Remove(dateRange);
            }
        }
        private List<DateRange> GetAllPossibleDates(DateTime StartDay, DateTime EndDay)
        {
            List<DateRange> result = new List<DateRange>();
            for (var day = StartDay; day.Date <= EndDay; day = day.AddDays(1))
            {
                if (day.AddDays(ReservationDays).Date <= EndDay)
                {
                    DateRange range = new DateRange(day.Date, day.AddDays(ReservationDays).Date);
                    result.Add(range);
                }
            }
            return result;
        }

        private void ShowFreeDatesForReservation()
        {
            List<DateRange> freeDates = ExtractFreeDates(StartDay, EndDay);
            if (freeDates.Count == 0)
            {
                NoFreeReservation.Visibility = Visibility.Visible;
                DateRanges = new ObservableCollection<DateRange>(ExtractFreeDates(DateTime.Now, DateTime.Now.AddDays(90)));
            }
            else
            {
                DateRanges = new ObservableCollection<DateRange>(freeDates);
            }
        }

        private void ReserveAccommodation(int accommodationId, int userId, DateRange dateRange)
        {
            Renovation renovation = new Renovation(accommodationId, userId, dateRange);
            //SelectedAccommodation.Reservations.Add(reservation);
            Renovations.Add(renovation);
            DateRanges.Clear();
            ShowFreeDatesForReservation();
            _renovationRepository.Save(renovation);

        }
    }
}
