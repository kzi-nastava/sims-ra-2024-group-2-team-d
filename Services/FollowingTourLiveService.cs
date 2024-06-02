using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class FollowingTourLiveService : IFollowingTourLiveService
    {
        public IFollowingTourLiveRepository FollowingTourLiveRepository { get; set; }

        public FollowingTourLiveService()
        {
            FollowingTourLiveRepository = Injector.Injector.CreateInstance<IFollowingTourLiveRepository>();
        }

        public FollowingTourLive Update(FollowingTourLive followingTourLive)
        {
            return FollowingTourLiveRepository.Update(followingTourLive);
        }

        public FollowingTourLive Save(FollowingTourLive followingTourLive)
        {
            return FollowingTourLiveRepository.Save(followingTourLive);
        }

        public List<FollowingTourLive> GetByTourInstanceId(int id)
        {
            return FollowingTourLiveRepository.GetByTourInstanceId(id);
        }

        public int GetKeyPoint(int id, int touristId)
        {
            List<FollowingTourLive> list = GetByTourInstanceId(id);
            int keypoint = -1;
            foreach (var item in list)
            {
                foreach (var item2 in item.TouristsIds)
                {
                    if (item2 == touristId)
                    {
                        keypoint = item.KeyPointId; break;
                    }
                }
            }
            return keypoint;
        }

        public FollowingTourLive? GetByTouristAndTourInstanceId(int touristId, int tourInstanceId)
        {
            return FollowingTourLiveRepository.GetByTouristAndTourInstanceId(touristId, tourInstanceId);
        }
    }
}
