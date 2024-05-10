using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class FollowingTourLiveService
    {
        public FollowingTourLiveRepository FollowingTourLiveRepository { get; set; }

        public FollowingTourLiveService() 
        {
            FollowingTourLiveRepository = new FollowingTourLiveRepository();
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
    }
}
