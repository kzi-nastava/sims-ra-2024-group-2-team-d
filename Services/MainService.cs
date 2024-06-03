using BookingApp.Services.IServices;

namespace BookingApp.Services
{
    public class MainService
    {
        private static MainService instance;

        private static readonly object LockObject = new object();

        //public static MainService Instance { get => Instance; set => Instance = value; }

        public ITourInstanceService TourInstanceService { get; set; }
        public ITourService TourService { get; set; }
        public ITourReservationService TourReservationService { get; set; }
        public IGiftCardService GiftCardService { get; set; }
        public ITourReviewService TourReviewService { get; set; }
        public ITouristService TouristService { get; set; }
        public IFollowingTourLiveService FollowingTourLiveService { get; set; }
        public IKeyPointService KeyPointService { get; set; }
        public ITourRequestService TourRequestService { get; set; }
        public IUserService UserService { get; set; }
        public IPictureService PictureService { get; set; }
        public IComplexTourRequestService ComplexTourRequestService{ get; set; }
        public ITouristNotificationsService TouristNotificationsService { get; set; }

        public MainService()
        {
            TourInstanceService = Injector.Injector.CreateInstance<ITourInstanceService>();
            TourService = Injector.Injector.CreateInstance<ITourService>();
            TourReservationService = Injector.Injector.CreateInstance<ITourReservationService>();
            GiftCardService = Injector.Injector.CreateInstance<IGiftCardService>();
            TourReviewService = Injector.Injector.CreateInstance<ITourReviewService>();
            TouristService = Injector.Injector.CreateInstance<ITouristService>();
            FollowingTourLiveService =  Injector.Injector.CreateInstance<IFollowingTourLiveService>();
            KeyPointService = Injector.Injector.CreateInstance<IKeyPointService>();
            TourRequestService =  Injector.Injector.CreateInstance<ITourRequestService>();
            UserService = Injector.Injector.CreateInstance<IUserService>();
            PictureService = Injector.Injector.CreateInstance<IPictureService>();
            ComplexTourRequestService = Injector.Injector.CreateInstance<IComplexTourRequestService>();
            TouristNotificationsService = Injector.Injector.CreateInstance<ITouristNotificationsService>();
        }

        public static MainService GetInstance()
        {
            lock (LockObject)
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
