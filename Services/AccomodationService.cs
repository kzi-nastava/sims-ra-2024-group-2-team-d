using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services
{
    public class AccomodationService : IAccommodationService
    {
        public IAccommodationRepository _repo { get; set; }
        public IReservationRepository _reservationRepository { get; set; }

        public AccomodationService()
        {

            _repo = Injector.Injector.CreateInstance<IAccommodationRepository>();
            _reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();

        }

        public List<Accommodation> GetAllOwnerAccommodations(int userId)
        {
            return _repo.GetAll().Where(a => a.UserId == userId).ToList();
        }

        public void Delete(Accommodation accommodation)
        {
            _repo.Delete(accommodation);
        }

        public int getOwnerIdByAccommodationId(int accommodationId)
        {
            return _repo.getAccommodation().Find(a => a.Id == accommodationId).UserId;
        }

        public string getNameById(int accommodationId)
        {
            return _repo.getAccommodation().Find(a => a.Id == accommodationId).Name;
        }

        public Accommodation GetById(int accommodationId)
        {
            return _repo.GetById(accommodationId);
        }

        public int GetAccommodationIdByReservationId(int reservationId)
        {
            return _reservationRepository.GetById(reservationId).AccomodationId;
        }

        public Accommodation GetAccommodationByReservationId(int reservationId)
        {
            var accomodationID = _reservationRepository.GetById(reservationId).AccomodationId;
            return _repo.GetById(accomodationID);
        }

        public bool HasAccommodationOnLocation(int ownerId, Location location)
        {
            List<Accommodation> accommodations = GetAccommodationsByOwnerId(ownerId);
            foreach (Accommodation accommodation in accommodations)
            {
                if (IsMatchingLocation(accommodation, location))
                {
                    return true;
                }
            }
            return false;
        }

        public List<Accommodation> GetAccommodationsByOwnerId(int ownerId)
        {
            var _accommodations = _repo.GetAll();
            return _accommodations.Where(a => a.UserId== ownerId).ToList();
        }

        private bool IsMatchingLocation(Accommodation accommodation, Location location)
        {
            return accommodation.Location.Country == location.Country && accommodation.Location.City == location.City;
        }
    }
}
