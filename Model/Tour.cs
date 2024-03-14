using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using BookingApp.Storage.Serializer;
using System.Net;

namespace BookingApp.Model
{
    public class Tour :ISerializable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location {  get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public List<KeyPoint> KeyPoints {  get; set; }
        public List<DateTime> Date {  get; set; }
        public int Duration { get; set; }
        public List<String> Pictures { get; set; }
        public int EmptySpots { get; set; }
        public bool Start {  get; set; }
        

        public Tour() { }

        public Tour(string title, string description, string location, string language, int maxTourists, List<KeyPoint> keyPoints, List<DateTime> date, int duration, List<String> pictures)
        {
            Title = title;
            Description = description;
            Location = location;
            Language = language; 
            MaxTourists = maxTourists;
            KeyPoints = keyPoints;
            Date = date;
            Duration = duration;
            Pictures = pictures;
            Start = false;

        }

        public override string ToString()
        {
            return $"ID: {Id,2} | Naziv: {Title,9} | Opis: {Description,9} | Mesto: {Location,10} | Jezik: {Language,5} | Maksimalan broj turista: {MaxTourists,3} | " +
                $"Kljucne tacke: {KeyPoints,20} | Datum i vreme: {Date,15} | Trajanje: {Duration,3} | Slike: {Pictures,9} |";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Title,
                Description,
                Location,
                Language,
                MaxTourists.ToString(),
                Duration.ToString()
                
                
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Title = values[1];
            Description = values[2];
            Location = values[3];
            Language = values[4];
            MaxTourists = int.Parse(values[5]);
            Duration = int.Parse(values[6]);
            KeyPoints = new List<KeyPoint>() {};
           
        }



    }
}

