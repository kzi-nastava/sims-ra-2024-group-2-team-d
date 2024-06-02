using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Model
{
    public class Location : ISerializable
    {
        public int Id { get; set; } 
        public string City { get; set; }
        public string Country { get; set; }

        public Location()
        {
        }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }



        public override string ToString()
        {
            return City + ";" + Country;
        }

        public Location FromStringToLocation(string s)
        {
            string[] locations = new string[2];
            locations = s.Split(';');
            return new Location(locations[0], locations[1]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                City,
                Country};

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}
