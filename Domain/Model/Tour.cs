using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using BookingApp.Storage.Serializer;
using System.Net;
using BookingApp.Repository;
using System.Globalization;

namespace BookingApp.Domain.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public List<DateTime> Dates { get; set; }
        public int Duration { get; set; }
        public List<string> Pictures { get; set; }
        // public int EmptySpots { get; set; }
        // public bool Start {  get; set; }
        public TourRepository _repository;
        public List<TourInstance> TourInstances { get; set; }
        public List<Picture> ClassPictures { get; set; }


        public Tour()
        {

            KeyPoints = new List<KeyPoint>();
            Dates = new List<DateTime>();
            Pictures = new List<string>();
        }

        public Tour(int userId, string title, string description, string location, string language, int maxTourists, string keyPoints, string dates, int duration, string pictures)
        {
            _repository = new TourRepository();
            Id = _repository.NextId();
            UserId = userId;
            Title = title;
            Description = description;
            Location = location;
            Language = language;
            MaxTourists = maxTourists;
            int order = 1;
            string[] parts = keyPoints.Split(',');
            KeyPoints = new List<KeyPoint>();
            foreach (string part in parts)
            {
                KeyPoints.Add(new KeyPoint(Id, part, order));
                order++;

            }

            string[] fragments = pictures.Split(",").Select(s => s.Trim()).ToArray(); ;
            Pictures = new List<string>();
            ClassPictures = new List<Picture>();
            foreach (string fragment in fragments)
            {
                Pictures.Add(fragment);
                ClassPictures.Add(new Picture(Id, fragment));
            }
            TourInstances = new List<TourInstance>();
            string format = "dd/MM/yyyy HH:mm";
            string[] slices = dates.Split(",").Select(s => s.Trim()).ToArray(); ;
            Dates = new List<DateTime>();
            foreach (string slice in slices)
            {
                DateTime parsedDate;
                DateTime.TryParseExact(slice, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                Dates.Add(parsedDate);
                TourInstance tourInstance = new TourInstance(Id, parsedDate, maxTourists);
                TourInstances.Add(tourInstance);
            }
            Duration = duration;
        }

        public Tour(string title, string description, string location, string language, int maxTourists, List<KeyPoint> keyPoints, List<DateTime> date, int duration, List<string> pictures)
        {
            Title = title;
            Description = description;
            Location = location;
            Language = language;
            MaxTourists = maxTourists;
            KeyPoints = keyPoints;
            Dates = date;
            Duration = duration;
            Pictures = pictures;
            // Start = false;

        }

        /* public override string ToString()
         {
             return $"ID: {Id,2} | Naziv: {Title,9} | Opis: {Description,9} | Mesto: {Location,10} | Jezik: {Language,5} | Maksimalan broj turista: {MaxTourists,3} | " +
                 $"Kljucne tacke: {KeyPoints,20} | Datum i vreme: {Dates,15} | Trajanje: {Duration,3} | Slike: {Pictures,9} |";
         }*/

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                UserId.ToString(),
                Title,
                Description,
                Location,
                Language,
                MaxTourists.ToString(),
                Duration.ToString(),



            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            Title = values[2];
            Description = values[3];
            Location = values[4];
            Language = values[5];
            MaxTourists = int.Parse(values[6]);
            Duration = int.Parse(values[7]);
            KeyPoints = new List<KeyPoint>() { };

        }



    }
}

