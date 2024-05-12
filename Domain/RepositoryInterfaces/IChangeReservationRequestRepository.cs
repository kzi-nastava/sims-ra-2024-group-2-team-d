using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IChangeReservationRequestRepository
    {
        public List<ChangeReservationRequest> GetAll();
        public ChangeReservationRequest Save(ChangeReservationRequest request);
        public int NextId();
        public void Delete(ChangeReservationRequest request);
        public ChangeReservationRequest Update(ChangeReservationRequest request);

        public List<ChangeReservationRequest> GetAllByUser(int userId);

        public ChangeReservationRequest GetById(int requestId);

        public List<ChangeReservationRequest> GetAllPendingReservationRequests(List<int> accomodationsIds);
        public void AcceptRequest(int requestId);

        public void DeclineRequest(int requestId, string comment);

    }
}
