using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using BookingApp.View.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services
{
    public class ReservationService : IReservationService
    {

        public IReservationRepository _repository { get; set; }
        public IAccommodationService _accommodationService{ get; set; }

        public ReservationService()
        {

            _repository = Injector.Injector.CreateInstance<IReservationRepository>();
            _accommodationService = Injector.Injector.CreateInstance<IAccommodationService>();
        }

        public List<Reservation> GetAll()
        {
            return _repository.GetAll();
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
            return _repository.GetReservation().Where(r => r.ReviewedByOwner == true && r.ReviewedByGuest == true).ToList();
        }

        public List<Reservation> GetAllUserReservations(int userId)
        {
            return _repository.GetReservation().Where(r => r.UserId == userId).ToList();
        }

        public Reservation GetById(int id)
        {
            return _repository.GetReservation().Where(r => r.Id == id).FirstOrDefault();
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

        public bool WasOnLocation(int userId, Location location)
        {
            List<Reservation> userReservations = GetAllUserReservations(userId);
            foreach (Reservation reservation in userReservations)
            {
                Accommodation accommodation = _accommodationService.GetAccommodationByReservationId(reservation.Id);
                if (IsMatchingLocation(accommodation, location))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsMatchingLocation(Accommodation accommodation, Location location)
        {
            return accommodation.Location.City == location.City && accommodation.Location.Country == location.Country;
        }

        public List<int> GetReservationsIdsByAccommodationId(int accommodationId)
        {
            List<int> reservationIds = new List<int>();
            List<Reservation> reservations = _repository.GetAll();
            foreach (Reservation ar in reservations)
            {
                if (ar.AccomodationId == accommodationId)
                {
                    reservationIds.Add(ar.Id);
                }
            }
            return reservationIds;
        }
    }
}
