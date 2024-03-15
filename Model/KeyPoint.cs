using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
//using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class KeyPoint : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TourId { get; set; }
        //public Tour Tour { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }

        public KeyPoint() { }

        public KeyPoint(int id, string name, int order)
        {
            Id = id;
            Name = name;
            Status = false;
            Order = order;
        }

        public override string ToString()
        {
            return $"ID: {Id,2} | Naziv: {Name,9} |";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
        }

    

    }
}
