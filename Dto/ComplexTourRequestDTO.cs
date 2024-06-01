using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using System.ComponentModel;
using System.Xml.Linq;

namespace BookingApp.Dto
{
    public class ComplexTourRequestDTO: INotifyPropertyChanged
    {
        public List<string> UniqueLanguages {  get; set; }
        public List<TourRequest> TourRequests { get; set; }

        private string currentStatus { get; set; }

        public string CurrentStatus
        {
            get => currentStatus;
            set
            {
                if (value != currentStatus)
                {
                    currentStatus = value;
                    OnPropertyChanged("CurrentStatus");
                }
            }
        }
        public List<string> Locations { get; set; }

        public int NumberOfRequests {  get; set; }
        public ComplexTourRequestDTO() { }

        public ComplexTourRequestDTO(ComplexTourRequest complexTourRequest)
        {
            TourRequests = complexTourRequest.TourRequests;
            CurrentStatus = StatusToString(complexTourRequest.CurrentStatus);
            UniqueLanguages = TourRequests.Select(tr => tr.Language)  
                             .Distinct()                 // Filtriraj jedinstvene jezike
                                .Take(5)                    // Uzmi prvih 5
                                .ToList();
            Locations = TourRequests.Select (tr => tr.Location) .Take(5).ToList();
            NumberOfRequests = TourRequests.Count();
        }

        public string StatusToString(Status currentStatus)
        {
            if (currentStatus == Status.OnHold)
            {
                return "On hold";
            }
            else if (currentStatus == Status.Accepted)
            {
                return "Accepted";
            }
            else
            {
                return "Invalid";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
