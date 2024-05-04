using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class LiveTourNotification : ISerializable
    
    {
        public int Id {  get; set; }

        public List<int> TouristsId { get; set; }


        public LiveTourNotification(List<int> touristsId) { 
            TouristsId = touristsId;
        }

        public LiveTourNotification()
        {
            TouristsId = new List<int>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                string.Join(",", TouristsForCSV())

            };
            return csvValues;
        }
        public List<string> TouristsForCSV()
        {
            List<string> csvV = new List<string>();
            if (TouristsId == null)
            {
                return csvV;
                //TouristsIds = new List<int>();
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
            if (string.IsNullOrWhiteSpace(values[1]))
            {
                TouristsId = new List<int>();
            }
            else
            {
                string[] slices = values[1].Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray();
                TouristsId = new List<int>();

                foreach (string slice in slices)
                {
                    TouristsId.Add(int.Parse(slice));
                }
            }

        }
    }
}
