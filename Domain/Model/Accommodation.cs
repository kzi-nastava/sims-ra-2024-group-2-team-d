﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum AccommodationType { Apartman = 0, Kuca = 1, Koliba = 2 }
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int DaysBeforeCancelling { get; set; }
        public List<string>? Images { get; set; }
        public List<Reservation> Reservations { get; set; }

        public List<Renovation> Renovations { get; set; }

        public Accommodation()
        {
        }

        public Accommodation(int userId, string name, string city, string country, AccommodationType accommodationType, int maxGuestNumber, int minReservationDays, int daysBeforeCancelling, List<string> images, List<Reservation> reservations)
        {
            Id = Id;
            UserId = userId;
            Name = name;
            Location = new Location(city, country);
            AccommodationType = accommodationType;
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
                UserId.ToString(),
                Name,
                Location.ToString(),
                AccommodationType.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                DaysBeforeCancelling.ToString(),
                SerializeImages(Images)};

            //string.Join(";", Images)
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Name = values[2];
            Location = fromStringToLocation(values[3]);
            AccommodationType = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[4]);
            MaxGuestNumber = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            DaysBeforeCancelling = Convert.ToInt32(values[7]);
            Images = values[8].Split(";").ToList();
        }

        private Location fromStringToLocation(string value)
        {
            Location location = new Location();
            string[] locations = new string[2];
            locations = value.Split(';');
            return new Location(locations[0], locations[1]);
        }

        private string SerializeImages(List<string> images)
        {
            if (images is null) return string.Empty;
            string list = string.Empty;
            foreach (var image in images)
            {
                list = list + image + ";";
            }
            return list;
        }
    }
}
