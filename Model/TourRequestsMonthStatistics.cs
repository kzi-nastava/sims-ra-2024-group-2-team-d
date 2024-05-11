using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TourRequestsMonthStatistics
    {
        //public int TourInstanceId { get; set; }
        public int Jan { get; set; }
        public int Feb { get; set; }
        public int Mar { get; set; }
        public int Apr { get; set; }
        public int May { get; set; }
        public int Jun { get; set; }
        public int Jul { get; set; }
        public int Aug { get; set; }
        public int Sep { get; set; }
        public int Oct { get; set; }
        public int Nov { get; set; }
        public int Dec { get; set; }

        public TourRequestsMonthStatistics()
        {
            Jan = 0; Feb = 0; Mar = 0; Apr = 0; May = 0; Jun = 0; Jul = 0; Aug = 0; Sep = 0; Oct = 0; Nov = 0; Dec = 0;
        }

        public TourRequestsMonthStatistics(int jan, int feb, int mar, int apr, int may, int jun, int jul, int aug, int sep, int oct, int nov, int dec)
        {
            Jan = jan; Feb= feb; Mar = mar; Apr = apr; May = may; Jun = jun; Jul = jul; Aug = aug; Sep = sep; Oct = oct; Nov = nov; Dec = dec;
        }
    }
}
