using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IFollowingTourLiveRepository
    {
        public FollowingTourLive Save(FollowingTourLive followingTourLive);

        public FollowingTourLive Update(FollowingTourLive followingTourLive);

        public List<FollowingTourLive> GetByTourInstanceId(int id);

        public FollowingTourLive GetByTourInstanceIdAndKeyPointId(int TourInstanceId, int KeyPointId);

        public int GetKeyPoint(int id, int touristId);
        public int GetKeyPointId(int id);

        public FollowingTourLive? GetByTouristAndTourInstanceId(int touristId, int tourInstanceId);
    }
}
