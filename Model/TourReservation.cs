using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using BookingApp.Serializer;


namespace BookingApp.Model
{
    public class TourReservation : ISerializable
    {
        public int Id {  get; set; }
        public int TourId { get; set; }
        public Tour ReservedTour { get; set; }

        public int TouristReservationNumber {  get; set; }
        public List<Tourist> Tourists { get; set; }

        public TourReservation() { }

        public TourReservation(int tourId, int numberOfReservations)
        {
            
            TourId = tourId;
            TouristReservationNumber = numberOfReservations;         
            
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                TouristReservationNumber.ToString(),

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            TouristReservationNumber = int.Parse(values[2]);           

        }
    }
}
