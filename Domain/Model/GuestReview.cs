using BookingApp.Serializer;
using System;
using System.Runtime.Serialization;

namespace BookingApp.Domain.Model
{
    public class GuestReview : Serializer.ISerializable
    {
        public int Id { get; set; }
        public int AccomodationId { get; set; }
        public int UserId { get; set; }
        public int Cleanness { get; set; }
        public int Rules { get; set; }
        public string Description { get; set; }

        public int ReservationId { get; set; }


        public GuestReview()
        {
        }

        public GuestReview(int accomodationId, int userId, int cleanness, int rules, string description,int reservationId )
        {
            AccomodationId = accomodationId;
            UserId = userId;
            Cleanness = cleanness;
            Rules = rules;
            Description = description;
            ReservationId = reservationId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                AccomodationId.ToString(),
                UserId.ToString(),
                Cleanness.ToString(),
                Rules.ToString(),
                Description,
                ReservationId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccomodationId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            Cleanness = Convert.ToInt32(values[3]);
            Rules = Convert.ToInt32(values[4]);
            Description = values[5];
            ReservationId= Convert.ToInt32(values[6]);
        }
    }
}
