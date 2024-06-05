using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IReservationService
    {

        List<Reservation> GetAll();
        List<Reservation> GetAllUnreviewed(List<int> accomodationId);

        List<Reservation> GetAllUnreviewedByGuest(int userId);

        List<Reservation> GetAllReviewedByBoth();

        List<Reservation> GetAllUserReservations(int userId);

        Reservation GetById(int id);

        DateTime GetCheckInDate(int userId, int reservationId);
        DateTime GetCheckOutDate(int userId, int reservationId);

        void ChangeReservationDateRange(DateTime newStartDate, DateTime newEndDate, int reservationId);
        bool WasOnLocation(int userId, Location location);
        List<int> GetReservationsIdsByAccommodationId(int accommodationId);
    }
}
