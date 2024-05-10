using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ChangeReservationRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/requests.csv";

        private readonly Serializer<ChangeReservationRequest> _serializer;

        private List<ChangeReservationRequest> _requests;

        public ChangeReservationRequestRepository()
        {
            _serializer = new Serializer<ChangeReservationRequest>();
            _requests = new List<ChangeReservationRequest>();
        }
        public List<ChangeReservationRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public ChangeReservationRequest Save(ChangeReservationRequest request)
        {
            request.RequestId = NextId();
            _requests.Add(request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }
        public int NextId()
        {
            _requests = _serializer.FromCSV(FilePath);
            if (_requests.Count < 1)
            {
                return 1;
            }
            return _requests.Max(r => r.RequestId) + 1;
        }
        public void Delete(ChangeReservationRequest request)
        {
            _requests = _serializer.FromCSV(FilePath);
            ChangeReservationRequest foundedRequest = _requests.Find(r => r.RequestId == request.RequestId);
            _requests.Remove(foundedRequest);
            _serializer.ToCSV(FilePath, _requests);
        }
        public ChangeReservationRequest Update(ChangeReservationRequest request)
        {
            _requests = _serializer.FromCSV(FilePath);
            ChangeReservationRequest founded = _requests.Find(r => r.ReservationId == request.ReservationId && r.UserId == request.UserId);
            int index = _requests.IndexOf(founded);
            _requests.Remove(founded);
            _requests.Insert(index, request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }

        public List<ChangeReservationRequest> GetAllByUser(int userId)
        {
            return GetAll().Where(r => r.UserId == userId).ToList();
        }

        public ChangeReservationRequest GetById(int requestId)
        {
            return GetAll().Where(r => r.RequestId == requestId).FirstOrDefault();
        }

        public List<ChangeReservationRequest> GetAllPendingReservationRequests(List<int> accomodationsIds)
        {
            return GetAll().Where(r => accomodationsIds.Contains(r.AccommodationId) && r.RequestStatus == StatusType.Pending).ToList();
        }

        public void AcceptRequest(int requestId)
        {
            var request = GetById(requestId);
            if (request != null)
            {
                request.RequestStatus = StatusType.Approved;
                Update(request);
            }
        }

        public void DeclineRequest(int requestId, string comment)
        {
            var request = GetById(requestId);
            if (request != null)
            {
                request.RequestStatus = StatusType.Canceled;
                request.OwnerComment = comment;
                Update(request);
            }
        }
    }
}
