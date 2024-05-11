using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BookingApp.Serializer;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Wpf;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Services
{
    public class TourRequestService
    {
        private ITourRequestRepository TourRequestRepository { get; set; }

        public TourRequestService(ITourRequestRepository tourRequestRepository) 
        { 
            TourRequestRepository = tourRequestRepository;
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
        public int CountRequestsOnLoc(string location)
        {
            return TourRequestRepository.GetAll().Where(x => x.Location == location).Count();
        }

        public int CountRequestsOnLocByYear(string location, int year)
        {
            return TourRequestRepository.GetAll().Where(x => x.Location == location && x.CreatedOn.Year == year).Count();
        }

        public int CountRequestsOnLocByYearAndMonth(string location, int year, int month)
        {
            return TourRequestRepository.GetAll().Where(x => x.Location == location && x.CreatedOn.Year == year && x.CreatedOn.Month == month).Count();
        }

        //public Dictionary<int, Dictionary<string, int>> CountRequestsOnLocByYears(string loc) //nisam znala kako da bindujem na grid
        //{
        //    var result = new Dictionary<int, Dictionary<string, int>>();
        //    List<TourRequest> tourRequests = new List<TourRequest>(TourRequestRepository.GetAll());
        //    foreach (var tourRequest in tourRequests)
        //    {
        //        int year = tourRequest.CreatedOn.Year;
        //        if (!result.ContainsKey(year))
        //        {
        //            result[year] = new Dictionary<string, int>();
        //        }
        //        if (!result[year].ContainsKey(tourRequest.Location))
        //        {
        //            result[year][tourRequest.Location] = 0;
        //        }
        //        if (tourRequest.Location == loc)
        //        {
        //            result[year][tourRequest.Location]++;
        //        }
        //    }
        //    return result;
        //}

        public double CalculateAverageNumberOfTourists(int userId)
        {
            List<TourRequest> userRequests = TourRequestRepository.GetAcceptedRequestsByUserId(userId);
            double average = userRequests.Sum(t => t.NumberOfTourists) / (double)userRequests.Count;
            return average;
        }

        public List<string> GetDistinctYearsForTourRequests(int userId)
        {
            List<TourRequest> userRequests = TourRequestRepository.GetAcceptedRequestsByUserId(userId);
            List<string> distinctYears = userRequests.Select(t => t.ChosenDateTime.Year).Distinct().Select(year => year.ToString()).ToList();
            return distinctYears;
        }

        public double CalculateAverageNumberOfTourists(int userId, string year)
        {
            if(year == "All years")
            {
                return CalculateAverageNumberOfTourists(userId);
            }
            List<TourRequest> userRequests = TourRequestRepository.GetAcceptedRequestsByUserIdAndYear(userId, int.Parse(year));
            double average = userRequests.Sum(t => t.NumberOfTourists) / (double)userRequests.Count;
            return average;
        }

        public SeriesCollection UpdatePie(int userId, string year)
        {
            List<TourRequest> tourRequests = new List<TourRequest>();
            if (year == "All years")
            {
                tourRequests = TourRequestRepository.GetByUserId(userId);

            }
            else
            {
                tourRequests = TourRequestRepository.GetByUserTouristIdAndYear(userId, int.Parse(year));
            }
            int acceptedRequests = tourRequests.Count(t => t.CurrentStatus == Status.Accepted);
            int notAcceptedRequests = tourRequests.Count(t => t.CurrentStatus != Status.Accepted);
            SeriesCollection PieSeriesCollection = new SeriesCollection
                                                        {
            new PieSeries { Title = "Accepted", Values = new ChartValues<int> { acceptedRequests }, DataLabels = true },
            new PieSeries { Title = "Not accepted", Values = new ChartValues<int> { notAcceptedRequests }, DataLabels = true }
                            };
            return PieSeriesCollection;
        }

        public Dictionary<string,int> GetLanguageRequestCountPair()
        {
            List<string> languages = TourRequestRepository.GetDistinctLanguages();
            Dictionary<string, int> languageCountPair = new Dictionary<string, int>();
            foreach (string language in languages)
            {
                languageCountPair[language] = TourRequestRepository.CountRequestsByLanguage(language);
            }
            return languageCountPair;
        }

        public Dictionary<string, int> GetLocationRequestCountPair() 
        {
            List<string> locations = TourRequestRepository.GetDistinctLocations();
            Dictionary<string, int> locationCountPair = new Dictionary<string, int>();
            foreach (string location in locations)
            {
                locationCountPair[location] = TourRequestRepository.CountRequestsByLocation(location);
            }
            return locationCountPair;
        }



    }
}