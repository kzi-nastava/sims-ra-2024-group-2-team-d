using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourReview : ISerializable
    {
        public int Id {  get; set; }

        public int UserId { get; set; }

        public int TourInstanceId { get; set; }

        public int GuideKnowledge {  get; set; }

        public int GuideLanguage { get; set; }

        public int Enjoyability { get; set; }

        public string Comment { get; set; }

        public int GuideId {  get; set; }

        public bool IsValid { get; set; }

        public TourReview()
        {
            IsValid = true;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                UserId.ToString(),
                TourInstanceId.ToString(),
                GuideKnowledge.ToString(),
                GuideLanguage.ToString(),
                Enjoyability.ToString(),
                IsValid.ToString(),
                GuideId.ToString(),
                Comment

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            TourInstanceId = int.Parse(values[2]);
            GuideKnowledge = int.Parse(values[3]);
            GuideLanguage = int.Parse(values[4]);
            Enjoyability = int.Parse(values[5]);
            IsValid = bool.Parse(values[6]);
            GuideId = int.Parse(values[7]);
            Comment = values[8];

        }
    }
}
