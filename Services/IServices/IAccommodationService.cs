using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IAccommodationService
    {
        List<Accommodation> GetAllOwnerAccommodations(int userId);

        void Delete(Accommodation accommodation);

        int getOwnerIdByAccommodationId(int accommodationId);

        string getNameById(int accommodationId);

        int GetAccommodationIdByReservationId(int reservationId);

        Accommodation GetAccommodationByReservationId(int reservationId);

        bool HasAccommodationOnLocation(int ownerId, Location location);

        List<Accommodation> GetAccommodationsByOwnerId(int ownerId);
        Accommodation GetById(int accommodationId);
    }
}
