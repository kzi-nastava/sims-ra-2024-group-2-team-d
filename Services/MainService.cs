using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class MainService
    {
        private static MainService instance;

        private static readonly object LockObject = new object();

        //public static MainService Instance { get => Instance; set => Instance = value; }

        public TourInstanceService TourInstanceService { get; set; }
        public TourService TourService { get; set; }
        public TourReservationService TourReservationService { get; set; }
        public GiftCardService GiftCardService { get; set; }
        public TourReviewService TourReviewService { get; set; }
        public TouristService TouristService { get; set; }
        public FollowingTourLiveService FollowingTourLiveService { get; set; }
        public KeyPointService KeyPointService { get; set; }
        public TourRequestService TourRequestService { get; set; }
        public UserService UserService { get; set; }
        public PictureService PictureService { get; set; }

        public MainService()
        {
            TourInstanceService = new TourInstanceService();
            TourService = new TourService();
            TourReservationService = new TourReservationService();
            GiftCardService = new GiftCardService();
            TourReviewService = new TourReviewService();
            TouristService = new TouristService();
            FollowingTourLiveService = new FollowingTourLiveService();
            KeyPointService = new KeyPointService();
            TourRequestService = new TourRequestService();
            UserService = new UserService();
            PictureService = new PictureService();
        }

        public static MainService GetInstance()
        {
            lock(LockObject)
            {
                if (instance == null)
                {
                    instance = new MainService();
                }
            }    

            return instance;
        }
    }
}
