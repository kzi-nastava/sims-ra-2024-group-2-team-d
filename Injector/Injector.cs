using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Services.IServices;
using System;
using System.Collections.Generic;

namespace BookingApp.Injector
{
    public static class Injector
    {
        private static readonly Dictionary<Type, Lazy<object>> _implementations = new Dictionary<Type, Lazy<object>>
        {
        { typeof(IAccommodationRepository), new Lazy<object>(() => new AccommodationRepository()) },
        { typeof(ITourRequestRepository),  new Lazy<object>(() => new TourRequestRepository())  },
        { typeof(ITouristNotificationsRepository), new Lazy<object>(() => new TouristNotificationsRepository()) },
        { typeof(IFollowingTourLiveRepository),new Lazy<object>(() => new FollowingTourLiveRepository())},
        { typeof(IGiftCardRepository), new Lazy<object>(() => new GiftCardRepository())  },
        { typeof(ITourRequestAcceptanceNotificationRepository), new Lazy<object>(() => new TourRequestAcceptanceNotificationRepository()) },
        { typeof(ITourCreationNotificationRepository), new Lazy<object>(() => new TourCreationNotificationRepository()) },
        { typeof(ITouristRepository), new Lazy<object>(() => new TouristRepository())},
        { typeof(ILiveTourNotificationRepository), new Lazy<object>(() => new  LiveTourNotificationRepository()) },
        { typeof(ITourReviewRepository), new Lazy<object>(() => new  TourReviewRepository())},
        { typeof(IAccommodationReviewRepository),  new Lazy<object>(() => new  AccommodationReviewRepository())},
        { typeof(IChangeReservationRequestRepository), new Lazy<object>(() => new  ChangeReservationRequestRepository())},
        { typeof(ICommentRepository), new  Lazy<object>(() => new  CommentRepository())},
        { typeof(IGuestReviewRepository), new Lazy<object>(() => new  GuestReviewRepository()) },
        { typeof(IRenovationRecommendationRepository), new Lazy<object>(() => new  RenovationRecommendationRepository()) },
        { typeof(IRenovationRepository), new Lazy<object>(() => new  RenovationRepository()) },
        { typeof(IReservationRepository), new Lazy<object>(() => new   ReservationRepository())},
        { typeof (IKeyPointRepository), new Lazy<object>(() => new   KeyPointRepository())},
        { typeof(IPictureRepository), new Lazy<object>(() => new   PictureRepository())},
        { typeof(ITourInstanceRepository), new Lazy<object>(() => new   TourInstanceRepository())},
        { typeof(ITourRepository), new Lazy<object>(() => new   TourRepository())},
        { typeof(ITourReservationRepository), new Lazy < object > (() => new TourReservationRepository())},
        { typeof(IUserRepository), new Lazy<object>(() => new   UserRepository())},
        { typeof(IComplexTourRequestRepository), new Lazy<object>(() => new   ComplexTourRequestRepository()) },
        { typeof(IForumRepository), new Lazy<object>(() => new   ForumRepository()) },
        { typeof(IForumCommentRepository), new Lazy<object>(() => new   ForumCommentRepository()) },
        { typeof(ILocationRepository), new Lazy<object>(() => new   LocationRepository()) },
        {typeof(ITourGiftCardAwardRecorderRepository), new Lazy<object>(() => new TourGiftCardAwardRecorderRepository())},
        {typeof(INotificationRepository), new Lazy<object>(() => new NotificationRespository())},

        
        // Service implementations are here
             {typeof(IAccommodationService), new Lazy<object>(() => new   AccomodationService()) },
             {typeof(IAccommodationReviewService), new Lazy<object>(() => new   AccommodationReviewService()) },
             {typeof(IChangeReservationRequestService), new Lazy<object>(() => new   ChangeReservationRequestService()) },
             {typeof(ICommentService), new Lazy<object>(() => new   CommentService()) },
             {typeof(IComplexTourRequestService), new Lazy<object>(() => new   ComplexTourRequestService()) },
             {typeof(IFollowingTourLiveService), new Lazy<object>(() => new   FollowingTourLiveService()) },
             {typeof(IForumCommentService), new Lazy<object>(() => new ForumCommentService()) },
             {typeof(IForumService), new  Lazy<object>(() => new   ForumService()) },
             {typeof(IForumUtilityService), new Lazy<object>(() => new   ForumUtilityService()) },
             {typeof(IGiftCardService), new Lazy<object>(() => new   GiftCardService()) },
             {typeof(IGuestReviewService), new Lazy<object>(() => new    GuestReviewService()) },
             {typeof(IKeyPointService), new Lazy<object>(() => new    KeyPointService()) },
             {typeof(ILiveTourNotificationService), new Lazy<object>(() => new    LiveTourNotificationService()) },
             {typeof(IPictureService), new Lazy < object > (() => new PictureService()) },
             {typeof(IRenovationRecommendationService), new Lazy < object > (() => new RenovationRecommendationService()) },
             {typeof(IRenovationService), new Lazy <object> (() => new RenovationService()) },
             {typeof(IReservationService), new  Lazy <object> (() => new ReservationService()) },    
             {typeof(ITourCreationNotificationService), new Lazy <object> (() => new TourCreationNotificationService()) },
             {typeof(ITourInstanceService), new Lazy <object> (() => new TourInstanceService()) },
             {typeof(ITouristNotificationsService), new Lazy <object> (() => new TouristNotificationsService()) },
             {typeof(ITouristService), new Lazy <object> (() => new TouristService()) },
             {typeof(ITourRequestAcceptanceNotificationService), new Lazy <object> (() => new TourRequestAcceptanceNotificationService()) },
             {typeof(ITourRequestService), new  Lazy <object> (() => new TourRequestService()) },
             {typeof(ITourReservationService), new Lazy < object > (() => new TourReservationService()) },
             {typeof(ITourReviewService), new Lazy < object > (() => new TourReviewService()) },

             {typeof(ITourService), new Lazy < object > (() => new TourService()) },
             {typeof(IUserService), new Lazy < object > (() => new UserService()) },
             {typeof(IForumIdService), new Lazy < object > (() => new ForumIdService()) },
             {typeof(ILocationService), new Lazy < object > (() => new LocationService()) },
             {typeof(INotificationService), new Lazy < object > (() => new NotificationService()) },
             {typeof(IStatisticService), new Lazy < object > (() => new StatisticService()) },
            
          
    };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type].Value;
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
