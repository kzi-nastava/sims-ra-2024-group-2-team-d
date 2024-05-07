using BookingApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class TourRequestDTO : INotifyPropertyChanged
    {
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
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
        public string Start { get; set; }
        public string End { get; set; }
        public int GuideId { get; set; }
        public List<int> TouristsId { get; set; }

        public int UserTouristId {  get; set; }
        
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

        public string CurrentStatus {  get; set; }

        public string ChosenDateTime {  get; set; }
        public TourRequestDTO(int userTouristId)
        {
            TouristsId = new List<int>();
            UserTouristId = userTouristId;
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
        }

        public TourRequest ToTourRequest()
        {
            return new TourRequest(Location, Description, Language, NumberOfTourists, DateOnly.Parse(Start), DateOnly.Parse(End), TouristsId, UserTouristId);
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





    }
}
