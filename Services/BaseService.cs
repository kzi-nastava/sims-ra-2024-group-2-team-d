using BookingApp.Interfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class BaseService
    {
        public AccomodationService AccomodationService { get; set; }

        public RenovationRecommendationService RenovationRecommendationService { get; set; }

        public AccommodationReviewService AccommodationReviewService { get; set; }

        public ChangeReservationRequestService ChangeReservationService { get; set; }

        public ReservationService ReservationService { get; set; }

        public GuestReviewService   GuestReviewService { get; set; }

        public RenovationService RenovationService { get; set; }

        private static object lockObject { get; set; } = new object();

        private static BaseService instance;
       

        public static BaseService getInstance()
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new BaseService();
                }

                return instance;
            }

        }
        
        public BaseService() {
            AccomodationService = new AccomodationService();
            AccommodationReviewService  = new AccommodationReviewService();
            ChangeReservationService = new ChangeReservationRequestService();
            ReservationService = new ReservationService();
            GuestReviewService = new GuestReviewService();
            RenovationService = new RenovationService();
            RenovationRecommendationService = new RenovationRecommendationService();        

        }


        public Dictionary<int, string> GetReservationsByUserId(int userId)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<Reservation> usersReservations = ReservationService.GetAllUserReservations(userId).Where(r => r.ReservationDateRange.StartDate >= DateTime.UtcNow).ToList();
            if (usersReservations.Count > 0)
            {
                foreach (Reservation reservation in usersReservations)
                {

                    Reservation founded = ReservationService.GetById(reservation.Id);
                    string value = "";
                    string accommodationName = AccomodationService.getNameById(reservation.AccomodationId);
                    value = value + " " + accommodationName + "; " + founded.ReservationDateRange.SStartDate + "-" + founded.ReservationDateRange.SEndDate;
                    result.Add(reservation.Id, value);
                }
                return result;
            }
            return result;

        }
    }
}
