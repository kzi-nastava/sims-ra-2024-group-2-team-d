using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class Tourist : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }

        public int ReservationId { get; set; }

        public int UserId { get; set; }

        public bool ShowedUp { get; set; }

        public bool IsNotified { get; set; }

        public Tourist()
        {
            UserId = -1;
        }

        public Tourist(string name, string lastName, int age)
        {
            Name = name;
            LastName = lastName;
            Age = age;
            UserId = -1;
            ShowedUp = false;
            IsNotified = false;
            ReservationId = -1;
        }

        public Tourist(string name, string lastName, int age, int userId)
        {
            Name = name;
            LastName = lastName;
            Age = age;
            UserId = userId;
            ShowedUp = false;
            IsNotified = false;
            ReservationId = -1;
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
                UserId.ToString(),
                ShowedUp.ToString(),
                IsNotified.ToString()
                //string.Join(",", GiftCardsForCSV())

            };
            return csvValues;
        }

        /*
        public List<string> GiftCardsForCSV()
        {
            List<string> csvV = new List<string>();
            foreach (int gc in GiftCardIds)
            {
                csvV.Add(gc.ToString());
            }
            return csvV;
        }
        */
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            LastName = values[2];
            Age = int.Parse(values[3]);
            ReservationId = int.Parse(values[4]);
            UserId = int.Parse(values[5]);
            ShowedUp = bool.Parse(values[6]);
            IsNotified = bool.Parse(values[7]);

        }
    }


}
