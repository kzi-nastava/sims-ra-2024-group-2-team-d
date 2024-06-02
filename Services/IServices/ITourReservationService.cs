using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface ITourReservationService
    {
        public List<TourReservation> GetAll();

        public int GetTourInstanceById(int id);

        public List<int> GetAllUsersByTourInstanceId(int id);

        public void BindTourists();

        public List<Tourist> GetAllTouristByTourId(int id);

        public TourReservation? GetByUserAndTourInstanceId(int tourInstanceId, int userId);

        public List<TourReservation> GetByUserId(int userId);
    }
}
