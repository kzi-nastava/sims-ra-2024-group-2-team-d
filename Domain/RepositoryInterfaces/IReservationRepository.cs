using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IReservationRepository
    {

        public List<Reservation> GetAll();

        public Reservation Save(Reservation Reservation);

        public int NextId();

        public void Delete(Reservation Reservation);

        public Reservation Update(Reservation Reservation);

        public List<Reservation> GetAllUnreviewed(List<int> accomodationId);

        public List<Reservation> GetAllUnreviewedByGuest(int userId);

        public List<Reservation> GetAllReviewedByBoth();

        public List<Reservation> GetAllUserReservations(int userId);

        public Reservation GetById(int id);

        public DateTime GetCheckInDate(int userId, int reservationId);
        public DateTime GetCheckOutDate(int userId, int reservationId);

        public Dictionary<int, string> GetReservationsByUserId(int userId);

        public void ChangeReservationDateRange(DateTime newStartDate, DateTime newEndDate, int reservationId);
    }
}
