using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequest.csv";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _tourRequest;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequest = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            if (_tourRequest.Count < 1)
            {
                return 1;
            }
            return _tourRequest.Max(c => c.Id) + 1;
        }

        public List<TourRequest> getByStatus()
        {
            return _tourRequest.Where(x => x.CurrentStatus == Status.OnHold).ToList();
        }

        public List<TourRequest> FilterByLocation(string location)
        {
            return _tourRequest.Where(x =>x.CurrentStatus==Status.OnHold && x.Location == location).ToList();
        }

        public List<TourRequest> FilterByLanguage(string language)
        {
            return _tourRequest.Where(x => x.CurrentStatus == Status.OnHold && x.Language == language).ToList();
        }

        public List<TourRequest> FilterByNumOfTourists(int num)
        {
            return _tourRequest.Where(x => x.CurrentStatus == Status.OnHold && x.NumberOfTourists == num).ToList();
        }

        public List<TourRequest> FilterByDateRange(DateOnly start, DateOnly end)
        {
            return _tourRequest.Where(x => x.CurrentStatus == Status.OnHold &&
            ((x.Start>=start && x.Start<=end) || (x.End<=end && x.End>=start))).ToList();
        }

        public string FindMostWantedLocInLastYear()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            List<TourRequest> tourRequests = new List<TourRequest>(_tourRequest.Where(x => x.CreatedOn >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1))).ToList());
            //tourRequests = _tourRequest.Where(x => x.CreatedOn >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1))).ToList();         
            foreach (var request in tourRequests)
            {
                if (counts.ContainsKey(request.Location))
                {
                    counts[request.Location]++;
                }
                else
                {
                    counts[request.Location] = 1;
                }
            }
            string location = counts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return location;
        }

        public string FindMostWantedLangInLastYear()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            List<TourRequest> tourRequests = new List<TourRequest>(_tourRequest.Where(x => x.CreatedOn >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1))).ToList());
            //tourRequests = _tourRequest.Where(x => x.CreatedOn >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1))).ToList();
            foreach (var request in tourRequests)
            {
                if (counts.ContainsKey(request.Language))
                {
                    counts[request.Language]++;
                }
                else
                {
                    counts[request.Language] = 1;
                }
            }
            string language = counts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return language;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequest = _serializer.FromCSV(FilePath);
            _tourRequest.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequest);
            return tourRequest;
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            TourRequest current = _tourRequest.Find(c => c.Id == tourRequest.Id);
            int index = _tourRequest.IndexOf(current);
            _tourRequest.Remove(current);
            _tourRequest.Insert(index, tourRequest);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourRequest);
            return tourRequest;
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequest;
        }

        public bool CheckUserIsAvaliable(User user, DateTime dateTime)
        {
            if (_tourRequest.Where(x => x.CurrentStatus == Status.Accepted && x.GuideId == user.Id && x.ChosenDateTime == dateTime).ToList().Count != 0)
            {
                return false;
            }
            else return true;
        }

        public List<TourRequest> GetByUserTouristId(int userTouristId) { 
            return _tourRequest.Where(t => t.UserTouristId == userTouristId).ToList();
        }
    }
}
