using BookingApp.Domain.Model;
using BookingApp.Repository;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IFollowingTourLiveService
    {
        public FollowingTourLive Update(FollowingTourLive followingTourLive);

        public FollowingTourLive Save(FollowingTourLive followingTourLive);

        public List<FollowingTourLive> GetByTourInstanceId(int id);

        public int GetKeyPoint(int id, int touristId);

        public FollowingTourLive? GetByTouristAndTourInstanceId(int touristId, int tourInstanceId);
    }
}
