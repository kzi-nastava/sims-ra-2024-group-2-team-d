using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services
{
    public class ChangeReservationRequestService : IChangeReservationRequestService
    {
        public IChangeReservationRequestRepository _repo { get; set; }

        public ChangeReservationRequestService()
        {

            _repo = Injector.Injector.CreateInstance<IChangeReservationRequestRepository>();
        }


        public List<ChangeReservationRequest> GetAllPendingReservationRequests(List<int> accomodationsIds)
        {
            return _repo.GetAll().Where(r => accomodationsIds.Contains(r.AccommodationId) && r.RequestStatus == StatusType.Pending).ToList();
        }

        public void AcceptRequest(int requestId)
        {
            var request = _repo.GetById(requestId);
            if (request != null)
            {
                request.RequestStatus = StatusType.Approved;
                _repo.Update(request);
            }
        }

        public void DeclineRequest(int requestId, string comment)
        {
            var request = _repo.GetById(requestId);
            if (request != null)
            {
                request.RequestStatus = StatusType.Canceled;
                request.OwnerComment = comment;
                _repo.Update(request);
            }
        }

    }
}
