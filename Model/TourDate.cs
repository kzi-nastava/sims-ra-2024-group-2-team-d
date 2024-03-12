using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookingApp.Model
{
    internal class TourDate
    {
        public int TourId { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; } 

        public TourDate() { }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                Date.ToString("yyyy-MM-dd HH:mm")
            
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            string format = "yyyy-MM-dd HH:mm";
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            Date.GetDateTimeFormats();
            
        }
    }
}
