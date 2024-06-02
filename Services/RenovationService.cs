using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services.IServices;
using System;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class RenovationService : IRenovationService
    {
        public RenovationRepository _repository { get; set; }

        public RenovationService()
        {

            _repository = new RenovationRepository();
        }

        public DateTime GetCheckInDate(int userId, int reservationId)
        {
            List<Renovation> renovations = _repository.GetAllUserReservations(userId);
            return renovations.Find(r => r.Id == reservationId).RenovationDateRange.StartDate;
        }
        public DateTime GetCheckOutDate(int userId, int reservationId)
        {
            List<Renovation> renovations = _repository.GetAllUserReservations(userId);
            return renovations.Find(r => r.Id == reservationId).RenovationDateRange.EndDate;
        }

        public Renovation Save(Renovation Renovation)
        {
            return _repository.Save(Renovation);
        }
        //obrisati

    }
}
