using BookingApp.Domain.Model;
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
        public TouristRepository TouristRepository { get; set; }

        public TourReservationService()
        {
            TourReservationRepository = new TourReservationRepository();
            TouristRepository = new TouristRepository();
        }

        public List<TourReservation> GetAll()
        {
            return TourReservationRepository.GetAll();
        }

        public int GetTourInstanceById(int id)
        {
            TourReservation TourReservation = GetAll().Find(r => r.Id == id);
            if (TourReservation != null)
            {
                return TourReservation.TourInstanceId;
            }
            else return -1;
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

        public void BindTourists()
        {
            foreach (var item in GetAll())
            {
                item.Tourists = TouristRepository.GetAllByTourReservationId(item.Id);
            }
        }

        public List<Tourist> GetAllTouristByTourId(int id)
        {
            List<Tourist> tourists = new List<Tourist>();
            BindTourists();
            List<TourReservation> list = GetAll().Where(r => r.TourInstanceId == id).ToList();
            foreach (var item in list)
            {
                foreach (var item1 in item.Tourists)
                {
                    if (!tourists.Contains(item1))
                    {
                        tourists.Add(item1);
                    }
                }
            }
            return tourists;
        }

        public TourReservation? GetByUserAndTourInstanceId(int tourInstanceId, int userId)
        {
            return TourReservationRepository.GetByUserAndTourInstanceId(tourInstanceId, userId);
        }

        public List<TourReservation> GetByUserId(int userId)
        {
            return TourReservationRepository.GetByUserId(userId);
        }
    }
}
