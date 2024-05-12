using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        public int NextId();

        public TourReservation Save(TourReservation tourReservation);

        public List<TourReservation> GetAll();

        public void BindTourists();

        public List<Tourist> GetAllTouristByTourId(int id);

        public int GetTourInstanceById(int id);
        //useri koji su napravili rezervacije za jednu istancu ture
        public List<int> GetAllUsersByTourInstanceId(int id);

        public TourReservation? GetByUserAndTourInstanceId(int tourInstanceId, int userId);

        public List<TourReservation> GetByUserId(int userId);
    }
}
