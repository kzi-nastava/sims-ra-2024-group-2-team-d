using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.View.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class ReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/Reservations.csv";

        private readonly AccommodationRepository _accommodationRepository;

        private readonly Serializer<Reservation> _serializer;

        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _reservations = _serializer.FromCSV(FilePath);
            _accommodationRepository = new AccommodationRepository();
        }

        public List<Reservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation Reservation)
        {
            Reservation.Id = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(Reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return Reservation;
        }

        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(a => a.Id) + 1;
        }

        public void Delete(Reservation Reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation founded = _reservations.Find(a => a.Id == Reservation.Id);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public Reservation Update(Reservation Reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation current = _reservations.Find(a => a.Id == Reservation.Id);
            int index = _reservations.IndexOf(current);
            _reservations.Remove(current);
            _reservations.Insert(index, Reservation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _reservations);
            return Reservation;
        }

        public List<Reservation> GetAllUnreviewed(List<int> accomodationId)
        {
            List<Reservation> list = new List<Reservation> ();
            foreach (var id in accomodationId)
            {
                foreach (var review in GetAll().Where(r => r.AccomodationId == id).Where(r => r.ReservationDateRange.EndDate <= System.DateTime.Today && r.ReservationDateRange.EndDate.AddDays(5) >= System.DateTime.Today).Where(r => r.ReviewedByOwner == false).ToList())
                {
                    list.Add(review);
                }

            }
            return list;
        }

        public List<Reservation> GetAllUnreviewedByGuest(int userId)
        {
            List<Reservation> list = new List<Reservation>();
            foreach (var review in GetAll().Where(r => r.UserId == userId).Where(r => r.ReservationDateRange.EndDate <= System.DateTime.Today && r.ReservationDateRange.EndDate.AddDays(5) >= System.DateTime.Today).Where(r => r.ReviewedByGuest == false).ToList())
            {
                list.Add(review);
            }

            return list;
        }

        public List<Reservation> GetAllReviewedByBoth()
        {
            return _reservations.Where(r => r.ReviewedByOwner == true && r.ReviewedByGuest == true).ToList();
        }

        public List<Reservation> GetAllUserReservations(int userId)
        {
            return _reservations.Where(r => r.UserId == userId).ToList();
        }

        public Reservation GetById(int id)
        {
            return _reservations.Where(r => r.Id == id).FirstOrDefault();
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

        public Dictionary<int, string> GetReservationsByUserId(int userId)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<Reservation> usersReservations = GetAllUserReservations(userId).Where(r=> r.ReservationDateRange.StartDate >= DateTime.UtcNow).ToList();
            if (usersReservations.Count > 0)
            {
                foreach (Reservation reservation in usersReservations)
                {
                    
                    Reservation founded = GetById(reservation.Id);
                    string value = "";
                    string accommodationName = _accommodationRepository.getNameById(reservation.AccomodationId);
                    value = value + " " + accommodationName + "; " + founded.ReservationDateRange.SStartDate + "-" + founded.ReservationDateRange.SEndDate;
                    result.Add(reservation.Id, value);
                }
                return result;
            }
            return null;

        }

        public void ChangeReservationDateRange(DateTime newStartDate, DateTime newEndDate, int reservationId)
        {
            Reservation reservation = GetById(reservationId);
            reservation.ReservationDateRange.StartDate = newStartDate;
            reservation.ReservationDateRange.EndDate = newEndDate;
            Update(reservation);
        }

    }
}
