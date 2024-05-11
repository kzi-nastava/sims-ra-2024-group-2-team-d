using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BookingApp.Dto
{
    public class AccommodationDto : INotifyPropertyChanged, IDataErrorInfo
    {

        public Accommodation ToModel()
        {
            Accommodation accommodation = new Accommodation(this.UserId, this.AccommodationName, this.AccommodationCity, this.AccommodationCountry, Type, this.AccommodationMaxGuests, this.AccommodationReservationMinDays, this.AccommodationCancelationDays, Images, Reservations);
            return accommodation;

        }

        public int UserId;

        public List<Reservation> Reservations;
        public List<string> Images;

        private string _accommodationName;

        private string _accommodationCity;

        private string _accommodationCountry;

        private int _maxGuests = 1;

        private int _minDays = 1;

        private int _cancelationDays = 1;

        private AccommodationType Type;
        private int _status;
        public int ApartmentType
        {
            get
            {
                return _status;
            }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    if (value == 0) Type = AccommodationType.Apartman;
                    else if (value == 1) Type = AccommodationType.Kuca;
                    else Type = AccommodationType.Koliba;
                    OnPropertyChanged("Type");
                }
            }

        }


        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged("AccomodationName");
                }
            }
        }

        public string AccommodationCity
        {
            get => _accommodationCity;
            set
            {
                if (value != _accommodationCity)
                {
                    _accommodationCity = value;
                    OnPropertyChanged("AccomodationCity");
                }
            }
        }

        public string AccommodationCountry
        {
            get => _accommodationCountry;
            set
            {
                if (value != _accommodationCountry)
                {
                    _accommodationCountry = value;
                    OnPropertyChanged("AccomodationCountry");
                }
            }
        }

        public int AccommodationMaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged("AccomodationMaxGuests");
                }
            }
        }

        public int AccommodationReservationMinDays
        {
            get => _minDays;
            set
            {
                if (value != _minDays)
                {

                    _minDays = value;
                    OnPropertyChanged("AccommodationReservationMinDays");
                }
            }
        }

        public int AccommodationCancelationDays
        {
            get => _cancelationDays;
            set
            {
                if (value != _cancelationDays)
                {
                    _cancelationDays = value;
                    OnPropertyChanged("AccommodationCancelationDays");
                }
            }
        }


        public string this[string columnName]
        {
            get
            {
                if (columnName == "AccomodationName")
                {
                    if (string.IsNullOrEmpty(AccommodationName))
                        return "Ime je obavezno";

                }
                else if (columnName == "AccomodationCity")
                {
                    if (string.IsNullOrEmpty(AccommodationCity))
                        return "Grad je obavezan";

                }
                else if (columnName == "AccomodationCountry")
                {
                    if (string.IsNullOrEmpty(AccommodationCountry))
                        return "Drzava je obavezna";

                }
                else if (columnName == "AccomodationMaxGuests")
                {
                    if (AccommodationCancelationDays <= 0)
                        return "Broj gostiju mora biti veci od 0";

                }
                else if (columnName == "AccommodationReservationMinDays")
                {
                    if (AccommodationCancelationDays <= 0)
                        return "Broj dana rezervacije mora biti veci od 0";

                }
                else if (columnName == "AccommodationCancelationDays")
                {
                    if (AccommodationCancelationDays<=0)
                        return "Broj dana za otkazivanje mora biti veci od 0";

                }
                return String.Empty;
            }
        }

        private readonly string[] _validatedProperties = { "AccomodationName", "AccomodationCountry", "AccomodationCity", "AccomodationMaxGuests", "AccommodationReservationMinDays", "AccommodationCancelationDays"};

        public string IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != "")
                        return this[property];
                }

                return "";
            }
        }

        public string Error => null;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
