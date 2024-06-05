using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IChangeReservationRequestService
    {
        public List<ChangeReservationRequest> GetAllPendingReservationRequests(List<int> accomodationsIds);

        public void AcceptRequest(int requestId);

        public void DeclineRequest(int requestId, string comment);
        List<ChangeReservationRequest> GetRequestsByAccommodationId(int accommodationId);
    }
}
