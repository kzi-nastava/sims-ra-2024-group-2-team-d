using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BookingApp.Dto
{
    public class AccommodationDto : INotifyPropertyChanged, IDataErrorInfo
    {

        public Accommodation ToModel()
        {
            Accommodation accommodation = new Accommodation(this.AccommodationName, this.AccommodationCity, this.AccommodationCountry, Type, this.AccommodationMaxGuests, this.AccommodationReservationMinDays, this.AccommodationCancelationDays, Images, Reservations);
            return accommodation;

        }

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
                    if (value == 0) Type = AccommodationType.Appartment;
                    else if (value == 1) Type = AccommodationType.House;
                    else Type = AccommodationType.Shack;
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


        public string this[string columnName] => throw new NotImplementedException();

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
