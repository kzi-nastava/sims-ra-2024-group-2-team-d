using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Tourist : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }

        public int ReservationId {  get; set; }

        public Tourist()
        {

        }

        public Tourist(string name, string lastName, int age)
        {
            Name = name;
            LastName = lastName;
            Age = age;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                LastName,
                Age.ToString(),
                ReservationId.ToString(),

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            LastName = values[2];
            Age = int.Parse(values[3]);
            ReservationId = int.Parse(values[4]);

        }
    }

    
}
