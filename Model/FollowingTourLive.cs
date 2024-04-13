using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class FollowingTourLive : ISerializable
    {
        public int Id { get; set; }
        public int TourInstanceId { get; set; }
        public int KeyPoint { get; set; }
        public List<int> TouristsIds { get; set;} //= new List<Tourist>();

        public FollowingTourLive() {}

        public FollowingTourLive(int tourInstanceId, int keyPoint)
        {
            TourInstanceId = tourInstanceId;
            KeyPoint = keyPoint;
            TouristsIds = new List<int>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourInstanceId.ToString(),
                KeyPoint.ToString(),
                string.Join(",", TouristsForCSV())

            };
            return csvValues;
        }

        public List<string> TouristsForCSV()
        {
            List<string> csvV = new List<string>();
            foreach (int t in TouristsIds)
                {
                    csvV.Add(t.ToString());
                }
            return csvV;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourInstanceId = int.Parse(values[1]);
            KeyPoint = int.Parse(values[2]);
            string[] slices = values[3].Split(",").Select(s => s.Trim()).ToArray();
            foreach(string slice in slices)
            {
                if (TouristsIds==null)
                {
                    TouristsIds = new List<int>();
                }
                TouristsIds.Add(int.Parse(slice));
            }

        }

}
}
