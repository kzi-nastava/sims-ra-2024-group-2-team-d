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
        public TourRequestDTO() {
            TouristsId = new List<int>();
        }

        public TourRequest ToTourRequest()
        {
            return new TourRequest(Location, Description, Language, NumberOfTourists, DateOnly.Parse(Start), DateOnly.Parse(End), TouristsId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
