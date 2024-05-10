using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class ReservationService
    {

        public ReservationRepository _repository { get; set; }

        public ReservationService()
        {

            _repository = new ReservationRepository();
        }


        public List<Reservation> GetAllUnreviewed(List<int> accomodationId)
        {
            List<Reservation> list = new List<Reservation>();
            foreach (var id in accomodationId)
            {
                foreach (var review in _repository.GetAll().Where(r => r.AccomodationId == id).Where(r => r.ReservationDateRange.EndDate <= System.DateTime.Today && r.ReservationDateRange.EndDate.AddDays(5) >= System.DateTime.Today).Where(r => r.ReviewedByOwner == false).ToList())
                {
                    list.Add(review);
                }

            }
            return list;
        }

        public List<Reservation> GetAllUnreviewedByGuest(int userId)
        {
            List<Reservation> list = new List<Reservation>();
            foreach (var review in _repository.GetAll().Where(r => r.UserId == userId).Where(r => r.ReservationDateRange.EndDate <= System.DateTime.Today && r.ReservationDateRange.EndDate.AddDays(5) >= System.DateTime.Today).Where(r => r.ReviewedByGuest == false).ToList())
            {
                list.Add(review);
            }

            return list;
        }

        public List<Reservation> GetAllReviewedByBoth()
        {
            return _repository.getReservation().Where(r => r.ReviewedByOwner == true && r.ReviewedByGuest == true).ToList();
        }

        public List<Reservation> GetAllUserReservations(int userId)
        {
            return _repository.getReservation().Where(r => r.UserId == userId).ToList();
        }

        public Reservation GetById(int id)
        {
            return _repository.getReservation().Where(r => r.Id == id).FirstOrDefault();
        }

        public DateTime GetCheckInDate(int userId, int reservationId)
        {
            List<Reservation> reservations = GetAllUserReservations(userId);
            return reservations.Find(r => r.Id == reservationId).ReservationDateRange.StartDate;
        }
        public DateTime GetCheckOutDate(int userId, int reservationId)
        {
            List<Reservation> reservations = GetAllUserReservations(userId);
            return reservations.Find(r => r.Id == reservationId).ReservationDateRange.EndDate;
        }

       

        public void ChangeReservationDateRange(DateTime newStartDate, DateTime newEndDate, int reservationId)
        {
            Reservation reservation = GetById(reservationId);
            reservation.ReservationDateRange.StartDate = newStartDate;
            reservation.ReservationDateRange.EndDate = newEndDate;
            _repository.Update(reservation);
        }
    }
}
