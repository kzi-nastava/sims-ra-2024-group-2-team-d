using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourGiftCardAwardRecorder: ISerializable
    {
        public int Id { get; set; }
        public int TouristUserId { get; set; }
        public DateOnly ReceivedDate { get; set; }
        public TourGiftCardAwardRecorder() { }

        public TourGiftCardAwardRecorder(int userId)
        {
            TouristUserId = userId;
            ReceivedDate = DateOnly.FromDateTime(DateTime.Now);
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReceivedDate = DateOnly.Parse(values[1]);
            TouristUserId = int.Parse(values[2]);

        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                ReceivedDate.ToString(),
                TouristUserId.ToString()
            };
            return csvValues;
        }
    }
}
