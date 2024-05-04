using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourReservationService
    {
        public TourReservationRepository TourReservationRepository { get; set; }

        public TourReservationService()
        {
            TourReservationRepository = new TourReservationRepository();
        }

        public List<TourReservation> GetAll()
        {
            return TourReservationRepository.GetAll();
        }

        public List<int> GetAllUsersByTourInstanceId(int id)
        {
            List<int> users = new List<int>();
            List<TourReservation> list = GetAll().Where(r => r.TourInstanceId == id).ToList();
            foreach (var item in list)
            {
                if (!users.Contains(item.UserId))
                {
                    users.Add(item.UserId);
                }
            }
            return users;
        }
    }
}
