using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
        { typeof(IAccommodationRepository), new AccommodationRepository() },
        { typeof(ITourRequestRepository), new TourRequestRepository() },
        { typeof(ITouristNotificationsRepository), new TouristNotificationsRepository()},
        { typeof(IFollowingTourLiveRepository), new FollowingTourLiveRepository()},
        { typeof(IGiftCardRepository), new GiftCardRepository() },
            { typeof(ITourRequestAcceptanceNotificationRepository), new TourRequestAcceptanceNotificationRepository() },
            { typeof(ITourCreationNotificationRepository), new TourCreationNotificationRepository() },
            {typeof(ITouristRepository), new TouristRepository()},
            {typeof(ILiveTourNotificationRepository), new LiveTourNotificationRepository() },
            { typeof(ITourReviewRepository), new TourReviewRepository()},
            { typeof(IAccommodationReviewRepository), new AccommodationReviewRepository() },
        { typeof(IChangeReservationRequestRepository), new ChangeReservationRequestRepository()},
        { typeof(ICommentRepository), new CommentRepository()},
        { typeof(IGuestReviewRepository), new GuestReviewRepository() },
        { typeof(IRenovationRecommendationRepository), new RenovationRecommendationRepository() },
        { typeof(IRenovationRepository), new RenovationRepository() },
        {typeof(IReservationRepository), new ReservationRepository()}

        
        // Add more implementations here
    };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
