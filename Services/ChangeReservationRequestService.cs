using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class ChangeReservationRequestService
    {
        public ChangeReservationRequestRepository _repo { get; set; }

        public ChangeReservationRequestService()
        {

            _repo = new ChangeReservationRequestRepository();
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
