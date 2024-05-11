using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourCreationNotification : ISerializable
    {
        public int Id { get; set; }

        public int CreatedTourInstanceId {  get; set; }

        public TourInstance CreatedTourInstance {  get; set; }

        public bool IsBasedOnLanguage {  get; set; }

        public bool IsBasedOnLocation {  get; set; }


        public TourCreationNotification() {
            IsBasedOnLanguage = false;
            IsBasedOnLocation = false;


        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                CreatedTourInstanceId.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CreatedTourInstanceId = int.Parse(values[1]);

        }
    }
}
