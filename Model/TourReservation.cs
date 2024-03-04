using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TourReservation
    {
        public int TourId { get; set; }
        public Tour ReservedTour { get; set; }
        public int TouristUserId {  get; set; }

        public List<Tourist> Tourists { get; set; }



    }
}
