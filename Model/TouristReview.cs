using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TouristReview 
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int KeyPointId { get; set;}
        public string KeyPointName { get; set; }
        public TourReview TourReview { get; set; }

        public TouristReview() { }



    }
}
