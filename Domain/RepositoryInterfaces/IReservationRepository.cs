using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservation();
        List<Reservation> GetAll();

        Reservation Save(Reservation Reservation);

        int NextId();

        void Delete(Reservation Reservation);

        Reservation Update(Reservation Reservation);

        List<Reservation> GetAllUnreviewed(List<int> accomodationId);

        List<Reservation> GetAllUnreviewedByGuest(int userId);

        List<Reservation> GetAllReviewedByBoth();

        List<Reservation> GetAllUserReservations(int userId);

        Reservation GetById(int id);

        DateTime GetCheckInDate(int userId, int reservationId);
        DateTime GetCheckOutDate(int userId, int reservationId);

        Dictionary<int, string> GetReservationsByUserId(int userId);

        void ChangeReservationDateRange(DateTime newStartDate, DateTime newEndDate, int reservationId);
    }
}
