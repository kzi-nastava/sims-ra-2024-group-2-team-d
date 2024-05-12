using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using BookingApp.Serializer;


namespace BookingApp.Domain.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public int TourInstanceId { get; set; }       

        public int UserId { get; set; }

        public List<Tourist> Tourists { get; set; }

        public TourReservation() { }

        public TourReservation(int tourId, int userId)
        {

            TourInstanceId = tourId;
            UserId = userId;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourInstanceId.ToString(),
                UserId.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourInstanceId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);

        }
    }
}
