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

        public TourInstance() {
            Start = false;
            BaseTour = new Tour();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                Date.ToString("yyyy-MM-dd HH:mm"),
                EmptySpots.ToString()
            
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
            
        }
    }
}
