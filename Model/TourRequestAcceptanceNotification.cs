using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourRequestAcceptanceNotification: ISerializable
    {
        public int Id {  get; set; }

        public int TourRequestId {  get; set; }
        public TourRequestAcceptanceNotification() { }

        public TourRequestAcceptanceNotification(int tourRequestId)
        {
            TourRequestId = tourRequestId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourRequestId.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourRequestId = int.Parse(values[1]);

        }

    }
}
