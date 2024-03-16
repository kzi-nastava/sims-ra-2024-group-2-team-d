using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Picture : ISerializable
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public int TourId {  get; set; }

        public Picture() { }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Path = values[1];
            TourId = int.Parse(values[2]);
           
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Path,
                TourId.ToString()
            };
            return csvValues;

        }
    }
}
