using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public enum Status { Accepted, OnHold, Invalid}
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public Status CurrentStatus { get; set; }
        public string Location {  get; set; }
        public string Description {  get; set; }
        public string Language {  get; set; }
        public int NumberOfTourists {  get; set; }
        public DateOnly Start {  get; set; }
        public DateOnly End { get; set; }
        public DateOnly CreatedOn {  get; set; }
        public DateTime ChosenDateTime { get; set; }
        public int GuideId { get; set; }

        public TourRequest()
        {
            CurrentStatus = Status.OnHold;
            GuideId = -1;
            ChosenDateTime = new DateTime(1900, 1, 1, 0, 0, 0);

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
                ChosenDateTime.ToString(),

            };
            return csvValues;
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

        }



    }
}
