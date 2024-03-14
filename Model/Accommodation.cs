using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public enum AccommodationType { Appartment = 0, House = 1, Shack = 2 }
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType accommodationType { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int DaysBeforeCancelling { get; set; }
        public List<string> Images { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Accommodation()
        {
        }

        public Accommodation(string name, string city, string country, AccommodationType accommodationType, int maxGuestNumber, int minReservationDays, int daysBeforeCancelling, List<string> images, List<Reservation> reservations)
        {
            Id = this.Id;
            Name = name;
            Location = new Location(city,country);
            this.accommodationType = accommodationType;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            DaysBeforeCancelling = daysBeforeCancelling;
            Images = images;
            Reservations = reservations;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                Location.ToString(),
                accommodationType.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                DaysBeforeCancelling.ToString()};

            //string.Join(";", Images)
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = fromStringToLocation(values[2]);
            accommodationType = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[3]);
            MaxGuestNumber = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            DaysBeforeCancelling = Convert.ToInt32(values[6]);
            //Images = values[7].Split(";").ToList<string>();
        }

        private Location fromStringToLocation(string value)
        {
            Location location = new Location();
            string[] locations = new string[2];
            locations = value.Split(';');
            return new Location(locations[0], locations[1]);
        }
    }
}
