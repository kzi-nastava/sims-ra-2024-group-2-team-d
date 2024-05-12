using BookingApp.Serializer;
using InitialProject.CustomClasses;
using System;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace BookingApp.Domain.Model
{
    public class Renovation : ISerializable
    {

        public int Id { get; set; }
        public int AccomodationId { get; set; }
        public int UserId { get; set; }
        public DateRange RenovationDateRange { get; set; }

        public DateTime _startDay { get; set; }

        public DateTime _endDay { get; set; }



        public string Comment { get; set; }

        public Accommodation Accomodation;

        public Renovation()
        {
        }

        public Renovation(int accomodationId, int userId, DateRange reservationDateRange)
        {
            AccomodationId = accomodationId;
            UserId = userId;
            RenovationDateRange = reservationDateRange;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccomodationId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            RenovationDateRange = fromStringToDateRange(values[3]);
            Comment = values[4];
            _startDay = RenovationDateRange.StartDate;
            _endDay = RenovationDateRange.EndDate;




        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                AccomodationId.ToString(),
                UserId.ToString(),
                RenovationDateRange.ToString(),
                Comment


            };
            return csvValues;

        }

        public DateRange fromStringToDateRange(string value)
        {

            string format = "dd.MM.yyyy. HH:mm:ss";
            string[] result = value.Split(",");
            return new DateRange(DateTime.ParseExact(result[0], format, CultureInfo.InvariantCulture), DateTime.ParseExact(result[1], format, CultureInfo.InvariantCulture));
        }

        public Accommodation GetAccommodationById(List<Accommodation> accommodations, int id)
        {
            foreach (Accommodation accommodation in accommodations)
            {
                if (accommodation.Id == id)
                {
                    return accommodation;
                }
            }
            return null; // ili bacite izuzetak ako smatrate da je to prikladnije
        }

    }
}
