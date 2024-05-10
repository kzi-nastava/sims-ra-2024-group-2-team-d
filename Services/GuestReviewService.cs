using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class GuestReviewService
    {
        public GuestReviewRepository _repository { get; set; }

        public GuestReviewService()
        {

            _repository = new GuestReviewRepository();
        }

    }
}
