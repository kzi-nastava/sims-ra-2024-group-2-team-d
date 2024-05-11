using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BookingApp.Serializer;
using BookingApp.Model;

namespace BookingApp.Services
{
    public class TourRequestService
    {
        private TourRequestRepository TourRequestRepository { get; set; }

        public TourRequestService() 
        { 
            TourRequestRepository = new TourRequestRepository();
        }

        public bool CheckUserIsAvaliable(User user, DateTime dateTime)
        {
            if (TourRequestRepository.GetAll().Where(x => x.CurrentStatus == Status.Accepted && x.GuideId == user.Id && x.ChosenDateTime == dateTime).ToList().Count != 0)
            {
                return false;
            }
            else return true;
        }

        public string FindMostWantedLocInLastYear()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            List<TourRequest> tourRequests = new List<TourRequest>(TourRequestRepository.GetAll().Where(x => x.CreatedOn >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1))).ToList());
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
            List<TourRequest> tourRequests = new List<TourRequest>(TourRequestRepository.GetAll().Where(x => x.CreatedOn >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1))).ToList());
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

        public TourRequest Update(TourRequest tourRequest)
        {
            return TourRequestRepository.Update(tourRequest);
        }

        public List<TourRequest> FilterByDateRange(DateOnly start, DateOnly end)
        {
            return TourRequestRepository.FilterByDateRange(start, end);
        }

        public List<TourRequest> FilterByLanguage(string language)
        {
            return TourRequestRepository.FilterByLanguage(language);
        }

        public List<TourRequest> FilterByNumOfTourists(int num)
        {
            return  TourRequestRepository.FilterByNumOfTourists(num);
        }

        public List<TourRequest> FilterByLocation(string location)
        {
            return TourRequestRepository.FilterByLocation(location);
        }

        public List<TourRequest> getByStatus()
        {
            return TourRequestRepository.getByStatus();
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return TourRequestRepository.Save(tourRequest);
        }

        public List<TourRequest> GetByUserTouristId(int userTouristId) {
            return TourRequestRepository.GetByUserTouristId(userTouristId);
        }

        public void InvalidateOutdatedTourRequests()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            List<TourRequest> tourRequests = TourRequestRepository.GetAll();
            for(int i=0; i<tourRequests.Count; i++)
            {
                DateOnly deadlineDate = tourRequests[i].End.AddDays(-2);
                if (today >= deadlineDate && tourRequests[i].CurrentStatus != Status.Accepted)
                {
                    tourRequests[i].CurrentStatus = Status.Invalid;
                    TourRequestRepository.Update(tourRequests[i]);
                }
            }
        }

        public List<int> FindUserIdsByLocation(string location)
        {
            List<TourRequest> tourRequests = TourRequestRepository.GetByLocation(location);
            List<int> uniqueUserTouristIds = tourRequests.Select(tr => tr.UserTouristId).Distinct().ToList(); 
            for(int i = 0; i < uniqueUserTouristIds.Count; i++)
            {
                if(TourRequestRepository.IsAcceptedAtLocationByUser(location, uniqueUserTouristIds[i]))
                {
                    uniqueUserTouristIds.RemoveAt(i);
                    i--;
                }
            }
            return uniqueUserTouristIds;

        }

        public List<int> FindUserIdsByLanguage(string language)
        {
            List<TourRequest> tourRequests = TourRequestRepository.GetByLanguage(language);
            List<int> uniqueUserTouristIds = tourRequests.Select(tr => tr.UserTouristId).Distinct().ToList();
            for (int i = 0; i < uniqueUserTouristIds.Count; i++)
            {
                if (TourRequestRepository.IsAcceptedInLanguageByUser(language, uniqueUserTouristIds[i]))
                {
                    uniqueUserTouristIds.RemoveAt(i);
                    i--;
                }
            }
            return uniqueUserTouristIds;

        }
    }
}
