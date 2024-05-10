using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;


namespace BookingApp.Model
{
    public class TouristsStatistics //: ISerializable
    {
        public int TourInstanceId { get; set; }
        public int Young { get; set; }
        public int Middle { get; set; }
        public int Old { get; set; }

        public TouristsStatistics()
        {
            Young = 0; Middle = 0; Old = 0;
        }

        public TouristsStatistics(int id, int young, int middle, int old)
        {
            TourInstanceId = id;
            Young = young;
            Middle = middle;
            Old = old;
        }



    }
}
