using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public enum Status { Accepted, OnHold, Invalid }
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public Status CurrentStatus { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int NumberOfTourists { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
        public DateOnly CreatedOn { get; set; }
        public DateTime ChosenDateTime { get; set; }
        public int GuideId { get; set; }
        public List<int> TouristsId { get; set; }

        public int UserTouristId { get; set; }

        public TourRequest()
        {
            CurrentStatus = Status.OnHold;
            GuideId = -1;
            ChosenDateTime = new DateTime(1900, 1, 1, 0, 0, 0);
            TouristsId = new List<int>();
        }

        public TourRequest(string location, string description, string language, int numberOfTourists, DateOnly start, DateOnly end, List<int> touristsId, int userTouristId)
        {
            CurrentStatus = Status.OnHold;
            Location = location;
            Description = description;
            Language = language;
            NumberOfTourists = numberOfTourists;
            Start = start;
            End = end;
            ChosenDateTime = new DateTime(1900, 1, 1, 0, 0, 0);
            GuideId = -1;
            TouristsId = touristsId;
            CreatedOn = DateOnly.FromDateTime(DateTime.Now);
            UserTouristId = userTouristId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                CurrentStatus.ToString(),
                Location,
                Language,
                Description,
                NumberOfTourists.ToString(),
                Start.ToString("yyyy-MM-dd"),
                End.ToString("yyyy-MM-dd"),
                CreatedOn.ToString("yyyy-MM-dd"),
                GuideId.ToString(),
                ChosenDateTime.ToString("yyyy-MM-dd"),
                string.Join(",", TouristsForCSV()),
                UserTouristId.ToString()
            };
            return csvValues;
        }

        public List<string> TouristsForCSV()
        {
            List<string> csvV = new List<string>();
            if (TouristsId == null)
            {
                return csvV;
            }
            foreach (int t in TouristsId)
            {
                csvV.Add(t.ToString());
            }
            return csvV;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CurrentStatus = (Status)Enum.Parse(typeof(Status), values[1]);
            Location = values[2];
            Language = values[3];
            Description = values[4];
            NumberOfTourists = int.Parse(values[5]);
            Start = DateOnly.Parse(values[6]);
            End = DateOnly.Parse(values[7]);
            CreatedOn = DateOnly.Parse(values[8]);
            GuideId = int.Parse(values[9]);
            ChosenDateTime = DateTime.Parse(values[10]);
            if (string.IsNullOrWhiteSpace(values[11]))
            {
                TouristsId = new List<int>();
            }
            else
            {
                string[] slices = values[11].Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray();
                TouristsId = new List<int>();

                foreach (string slice in slices)
                {
                    TouristsId.Add(int.Parse(slice));
                }
            }
            UserTouristId = int.Parse(values[12]);
        }
    }
}
