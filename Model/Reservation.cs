using BookingApp.Serializer;
using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class Reservation : ISerializable
    {

        public int Id { get; set; }
        public int AccomodationId {  get; set; }
        public int UserId { get; set; }
        public DateRange ReservationDateRange {  get; set; }
        public int NumberOfGuests {  get; set; }


        public Reservation()
        {
        }

        public Reservation(int accomodationId, int userId, DateRange reservationDateRange, int numberOfGuests)
        {
            AccomodationId = accomodationId;
            UserId = userId;
            ReservationDateRange = reservationDateRange;
            NumberOfGuests = numberOfGuests;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccomodationId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            ReservationDateRange = fromStringToDateRange(values[3]);
            NumberOfGuests = Convert.ToInt32(values[4]);

        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                AccomodationId.ToString(),
                UserId.ToString(),
                ReservationDateRange.ToString(),
                NumberOfGuests.ToString(),
            };
            return csvValues;

        }

        public DateRange fromStringToDateRange(string value)
        {
            string[] result = value.Split(",");
            return new DateRange(DateTime.Parse(result[0]), DateTime.Parse(result[1]));
        }
    }
}
