using BookingApp.Domain.Model;
using BookingApp.Repository;
using LiveCharts;
using System;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface ITourRequestService
    {
        public bool CheckUserIsAvaliable(User user, DateTime dateTime);

        public string FindMostWantedLocInLastYear();

        public string FindMostWantedLangInLastYear();

        public TourRequest Update(TourRequest tourRequest);

        public List<TourRequest> FilterByDateRange(DateOnly start, DateOnly end);

        public List<TourRequest> FilterByLanguage(string language);

        public List<TourRequest> FilterByNumOfTourists(int num);

        public List<TourRequest> FilterByLocation(string location);

        public List<TourRequest> getByStatus();

        public TourRequest Save(TourRequest tourRequest);

        public List<TourRequest> GetByUserTouristId(int userTouristId);

        public void InvalidateOutdatedTourRequests();

        public List<int> FindUserIdsByLocation(string location);

        public List<int> FindUserIdsByLanguage(string language);
        public int CountRequestsOnLoc(string location);

        public int CountRequestsOnLang(string lang);

        public int CountRequestsOnLocByYear(string location, int year);

        public int CountRequestsOnLangByYear(string lang, int year);

        public int CountRequestsOnLocByYearAndMonth(string location, int year, int month);

        public int CountRequestsOnLangByYearAndMonth(string lang, int year, int month);

        public double CalculateAverageNumberOfTourists(int userId);

        public List<string> GetDistinctYearsForTourRequests(int userId);

        public double CalculateAverageNumberOfTourists(int userId, string year);

        public SeriesCollection UpdatePie(int userId, string year);

        public Dictionary<string, int> GetLanguageRequestCountPair();

        public Dictionary<string, int> GetLocationRequestCountPair();

        public List<DateTime> FindAllUnavaliableDates(User user);

    }
}
