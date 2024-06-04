using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository
{
    public class TourRequestRepository : ITourRequestRepository
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

        public TourRequest GetById(int id)
        {
            return _tourRequest.Find(x => x.Id == id);
        }

        public List<TourRequest> getByStatus()
        {
            return _tourRequest.Where(x => x.CurrentStatus == Status.OnHold && x.IsPartOfComplexRequest == false).ToList();
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
            return _tourRequest.Where(x => x.CurrentStatus == Status.OnHold && ((x.Start>=start && x.Start<=end) || (x.End<=end && x.End>=start))).ToList();
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
            return _tourRequest.Where(t => t.UserTouristId == userTouristId && t.IsPartOfComplexRequest == false).ToList();
        }

        public List<TourRequest> GetByLocation(string location)
        {
            return _tourRequest.Where(t => String.Equals(t.Location, location, StringComparison.OrdinalIgnoreCase) && t.CurrentStatus != Status.Accepted).ToList();
        }

        public List<TourRequest> GetByLanguage(string language)
        {
            return _tourRequest.Where(t => String.Equals(t.Language, language, StringComparison.OrdinalIgnoreCase) && t.CurrentStatus != Status.Accepted).ToList();
        }

        public bool IsAcceptedAtLocationByUser(string location, int userId)
        {
            return _tourRequest.Any(t => String.Equals(t.Location, location, StringComparison.OrdinalIgnoreCase) && t.CurrentStatus == Status.Accepted && t.UserTouristId == userId);
        }

        public bool IsAcceptedInLanguageByUser(string language, int userId)
        {
            return _tourRequest.Any(t => String.Equals(t.Language, language, StringComparison.OrdinalIgnoreCase) && t.CurrentStatus == Status.Accepted && t.UserTouristId == userId);
        }

        public List<TourRequest> GetAcceptedRequestsByUserId(int userId)
        {
            return _tourRequest.Where(t => t.CurrentStatus == Status.Accepted && t.UserTouristId == userId && t.IsPartOfComplexRequest != true).ToList();
        }

        public List<TourRequest> GetAcceptedRequestsByUserIdAndYear(int userId, int year)
        {
            return _tourRequest.Where(t => t.CurrentStatus == Status.Accepted && t.UserTouristId == userId && t.ChosenDateTime.Year == year && t.IsPartOfComplexRequest != true).ToList();
        }

        public List<TourRequest> GetByUserId(int userId)
        {
            return _tourRequest.Where(t =>t.UserTouristId == userId).ToList();
        }

        public List<TourRequest> GetByUserTouristIdAndYear(int userTouristId, int year)
        {
            return _tourRequest.Where(t => t.UserTouristId == userTouristId && (t.Start.Year == year || t.End.Year == year)).ToList();
        }

        public List<string> GetDistinctLanguages()
        {
            return _tourRequest.Where(t => t.IsPartOfComplexRequest != true).Select(t => t.Language).Distinct().ToList();
        }

        public int CountRequestsByLanguage(string language)
        {
            return _tourRequest.Count(t => String.Equals(t.Language, language, StringComparison.OrdinalIgnoreCase) && t.IsPartOfComplexRequest != true);
        }

        public List<string> GetDistinctLocations()
        {
            return _tourRequest.Where(t => t.IsPartOfComplexRequest != true).Select(t => t.Location).Distinct().ToList();
        }

        public int CountRequestsByLocation(string location)
        {
            return _tourRequest.Count(t => String.Equals(t.Location, location, StringComparison.OrdinalIgnoreCase) && t.IsPartOfComplexRequest != true);
        }

        public List<TourRequest> GetByIds(List<int> ids)
        {
            return _tourRequest.Where(t => ids.Contains(t.Id)).ToList();
        }

        public bool IsEveryPartOfComplexTourAccepted(List<int> tourRequestIds)
        {
            var filteredRequests = _tourRequest.Where(t => tourRequestIds.Contains(t.Id));
            return !filteredRequests.Any(t => t.CurrentStatus != Status.Accepted);
        }

        public List<TourRequest> GetAllOutdatedPartsOfComplexTourRequest(List<int> tourRequestIds)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            return _tourRequest.Where(t => tourRequestIds.Contains(t.Id) && t.Start <= today.AddDays(2) && t.CurrentStatus != Status.Accepted).ToList();
        }

    }
}
