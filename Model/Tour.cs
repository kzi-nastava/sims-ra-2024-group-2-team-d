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

namespace BookingApp.Model
{
    public class Tour :ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location {  get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public List<KeyPoint> KeyPoints {  get; set; }
        public List<DateTime> Dates {  get; set; }
        public int Duration { get; set; }
        public List<String> Pictures { get; set; }
        // public int EmptySpots { get; set; }
        // public bool Start {  get; set; }
        public TourRepository _repository;
        public List<TourInstance> TourInstances { get; set; }
        public List<Picture> ClassPictures { get; set; }


        public Tour() {
            
            KeyPoints = new List<KeyPoint>();
            Dates = new List<DateTime>();
            Pictures = new List<String>(); 
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
            Pictures = new List<String>();
            ClassPictures = new List<Picture>();
            foreach (string fragment in fragments)
            {
                Pictures.Add(fragment);
                ClassPictures.Add(new Picture(Id, fragment));
            }

            //_instanceRepository = new TourInstanceRepository();
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
                //_instanceRepository.Save(tourInstance);
            }

            Duration = duration;
            //Pictures = pictures;
            // Start = false;

        }

        public Tour(string title, string description, string location, string language, int maxTourists, List<KeyPoint> keyPoints, List<DateTime> date, int duration, List<String> pictures)
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

