using BookingApp.Domain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Dto
{
    public class TourRequestDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        private int numberOfTourists;

        public int NumberOfTourists
        {
            get => numberOfTourists;
            set
            {
                if (value != numberOfTourists)
                {
                    numberOfTourists = value;
                    numberOfTouristsCounter = numberOfTourists-1;
                    OnPropertyChanged("NumberOfTourists");
                    OnPropertyChanged("NumberOfTouristsCounter");
                }
            }
        }
        
        private int numberOfTouristsCounter {  get; set; }

        public int NumberOfTouristsCounter
        {
            get => numberOfTouristsCounter;
            set
            {
                if (value != numberOfTouristsCounter)
                {
                    numberOfTouristsCounter = value;
                    OnPropertyChanged("NumberOfTouristsCounter");
                }
            }
        }
        public bool IsSecondDatePickerEnabled { get; set; }

        private bool IsFirstTime {  get; set; }

        private DateTime start;

        public DateTime Start
        {
            get => start;
            set
            {
                if(value != start) {
                    if (!IsFirstTime)
                    {
                        IsSecondDatePickerEnabled = true;
                    }
                    IsFirstTime = false;
                    start = value;
                    End = start.AddDays(1);
                    FirstAvailableDateForSecondDatePicker = start.AddDays(1);
                    OnPropertyChanged("Start");
                    OnPropertyChanged("End");
                    OnPropertyChanged("IsSecondDatePickerEnabled");
                    OnPropertyChanged("IsFirstTime");
                    OnPropertyChanged("FirstAvailableDateForSecondDatePicker");
                }
            }
        }
        private DateTime end;

        public DateTime End
        {
            get => end;
            set
            {
                if(value != end)
                {
                    end = value;
                    OnPropertyChanged("End");
                }
            }
        }
        public int GuideId { get; set; }
        public List<int> TouristsId { get; set; }

        public int UserTouristId { get; set; }

        public string CurrentStatus {  get; set; }

        public string ChosenDateTime {  get; set; }

        private string location;
        public string Location
        {
            get => location;
            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged("Location");
                }
            }
        }
        private string description;
        public string Description
        {
            get => description;
            set
            {
                if(value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        private string language;
        public string Language
        {
            get => language;
            set
            {
                if (value != language)
                {
                    language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private DateTime firstAvailableDate;

        public DateTime FirstAvailableDate
        {
            get => firstAvailableDate;
            set
            {
                if(firstAvailableDate != value)
                {
                    firstAvailableDate = value;
                    OnPropertyChanged("FirstAvailableDate");
                }
            }
        }

        public string StartEndDate {  get; set; }

        public List<Tourist> Tourists { get; set; }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == nameof(NumberOfTourists))
                {
                    Regex regex = new Regex("^[0-9]*$");  // Dozvoljava samo brojeve
                    if (!regex.IsMatch(NumberOfTourists.ToString()))
                    {
                        result = "Please enter a valid number.";
                    }
                }
                return result;
            }
        }

        public DateTime FirstAvailableDateForSecondDatePicker { get; set; }

        public TourRequestDTO(int userTouristId)
        {
            IsFirstTime = true;
            TouristsId = new List<int>();
            UserTouristId = userTouristId;
            IsSecondDatePickerEnabled = false;
            FirstAvailableDate = DateTime.Now.AddDays(3);
            Start = DateTime.Now.AddDays(3);
            End = Start.AddDays(1);
            NumberOfTourists = 1;
            Tourists = new List<Tourist>();
        }

        public TourRequestDTO(TourRequest tourRequest)
        {
            CurrentStatus = StatusToString(tourRequest.CurrentStatus);
            Location = tourRequest.Location;
            Description = tourRequest.Description;
            Language = tourRequest.Language;
            NumberOfTourists = tourRequest.NumberOfTourists;
            DateTime dateForComparison = new DateTime(1900, 1, 1, 0, 0, 0);
            if(dateForComparison == tourRequest.ChosenDateTime )
            {
                ChosenDateTime = "Not decided";
            }
            else
            {
                ChosenDateTime = tourRequest.ChosenDateTime.ToString();
            }
            TouristsId = tourRequest.TouristsId;
            Tourists = new List<Tourist>();
        }

        public TourRequest ToTourRequest()
        {
            return new TourRequest(Location, Description, Language, NumberOfTourists, DateOnly.FromDateTime(Start), DateOnly.FromDateTime(End), TouristsId, UserTouristId, Tourists);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string StatusToString(Status currentStatus)
        {
            if(currentStatus == Status.OnHold)
            {
                return "On hold";
            }else if(currentStatus == Status.Accepted)
            {
                return "Accepted";
            }
            else
            {
                return "Invalid";
            }
        }

        public void UpdateStartEndString()
        {
            StartEndDate = Start.ToString("dd.MM.yyyy") + "-" + End.ToString("dd.MM.yyyy");
        }

     



    }
}
