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

        public int TourId { get; set; }

        public int GuideKnowledge {  get; set; }

        public int GuideLanguage { get; set; }

        public int Enjoyability { get; set; }

        public string Comment { get; set; }

        public int GuideId {  get; set; }

        public TourReview()
        {

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                GuideKnowledge.ToString(),
                GuideLanguage.ToString(),
                Enjoyability.ToString(),
                GuideId.ToString(),
                Comment

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            GuideKnowledge = int.Parse(values[2]);
            GuideLanguage = int.Parse(values[3]);
            Enjoyability = int.Parse(values[4]);
            GuideId = int.Parse(values[5]);
            Comment = values[6];

        }
    }
}
