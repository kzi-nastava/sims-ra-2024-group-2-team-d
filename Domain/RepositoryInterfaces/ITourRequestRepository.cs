using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        public TourRequest GetById(int id);

        public List<TourRequest> getByStatus();

        public List<TourRequest> FilterByLocation(string location);

        public List<TourRequest> FilterByLanguage(string language);

        public List<TourRequest> FilterByNumOfTourists(int num);

        public List<TourRequest> FilterByDateRange(DateOnly start, DateOnly end);

        public string FindMostWantedLocInLastYear();

        public string FindMostWantedLangInLastYear();

        public TourRequest Save(TourRequest tourRequest);

        public TourRequest Update(TourRequest tourRequest);

        public List<TourRequest> GetAll();

        public bool CheckUserIsAvaliable(User user, DateTime dateTime);

        public List<TourRequest> GetByUserTouristId(int userTouristId);

        public List<TourRequest> GetByLocation(string location);

        public List<TourRequest> GetByLanguage(string language);

        public bool IsAcceptedAtLocationByUser(string location, int userId);

        public bool IsAcceptedInLanguageByUser(string language, int userId);

        public List<TourRequest> GetAcceptedRequestsByUserId(int userId);

        public List<TourRequest> GetAcceptedRequestsByUserIdAndYear(int userId, int year);

        public List<TourRequest> GetByUserId(int userId);

        public List<TourRequest> GetByUserTouristIdAndYear(int userTouristId, int year);

        public List<string> GetDistinctLanguages();

        public int CountRequestsByLanguage(string language);

        public List<string> GetDistinctLocations();

        public int CountRequestsByLocation(string location);
    }
}
