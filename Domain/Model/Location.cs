using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Location
    {
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
    }
}
