using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookingApp.Model
{
    public class TourInstance : ISerializable
    {
        public int TourId { get; set; }

        public Tour BaseTour {  get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EmptySpots { get; set; }
        public bool Start {  get; set; }
        public bool End { get; set; }
        
        public bool IsNotReviewed {  get; set; }

        public TourInstance(int tourId, DateTime date, int emptySpots)
        {
            Id = this.Id;
            TourId = tourId;
            Date = date;
            EmptySpots = emptySpots;
            Start = false;
            End = false;
            IsNotReviewed = true;
        }

        public TourInstance() {
            Start = false;
            End = false;
            BaseTour = new Tour();
            IsNotReviewed = true;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                Date.ToString("yyyy-MM-dd HH:mm"),
                EmptySpots.ToString(),
                Start.ToString(),
                End.ToString(),
                IsNotReviewed.ToString()
            
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            string format = "yyyy-MM-dd HH:mm";
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            Date = DateTime.Parse(values[2]);
            EmptySpots = int.Parse(values[3]);
            Start = bool.Parse(values[4]);
            End = bool.Parse(values[5]);
            IsNotReviewed = bool.Parse(values[6]);
            
        }
    }
}
