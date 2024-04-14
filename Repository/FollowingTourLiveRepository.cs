using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class FollowingTourLiveRepository
    {
        private const string FilePath = "../../../Resources/Data/followingToursLive.csv";

        private readonly Serializer<FollowingTourLive> _serializer;

        private List<FollowingTourLive> _followingToursLive;

        public FollowingTourLiveRepository()
        {
            _serializer = new Serializer<FollowingTourLive>();
            _followingToursLive = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _followingToursLive = _serializer.FromCSV(FilePath);
            if (_followingToursLive.Count < 1)
            {
                return 1;
            }
            return _followingToursLive.Max(c => c.Id) + 1;
        }

        public FollowingTourLive Save(FollowingTourLive followingTourLive)
        {
            followingTourLive.Id = NextId();
            _followingToursLive = _serializer.FromCSV(FilePath);
            _followingToursLive.Add(followingTourLive);
            _serializer.ToCSV(FilePath, _followingToursLive);
            return followingTourLive;
        }

        public FollowingTourLive Update(FollowingTourLive followingTourLive)
        {
            _followingToursLive = _serializer.FromCSV(FilePath);
            FollowingTourLive current = _followingToursLive.Find(c => c.Id == followingTourLive.Id);
            int index = _followingToursLive.IndexOf(current);
            _followingToursLive.Remove(current);
            _followingToursLive.Insert(index, followingTourLive);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _followingToursLive);
            return followingTourLive;
        }

        public  List<FollowingTourLive> GetByTourInstanceId(int id)
        {
            return _followingToursLive.Where(r=>r.TourInstanceId==id).ToList();
        }


        public int GetKeyPoint(int id,int touristId)
        {

            List<FollowingTourLive> list = GetByTourInstanceId(id);
            int keypoint = -1;
            foreach (var item in list)
            {
                foreach (var item2 in item.TouristsIds)
                {
                
                    if(item2==touristId)
                    {
                        keypoint = item.KeyPointId; break;
                    }
                }
            }

            return keypoint;
        }

        public int GetKeyPointId(int id)
        {

            return _followingToursLive.Find(r => r.TourInstanceId == id).KeyPointId;
        }

        public FollowingTourLive? GetByTouristAndTourInstanceId(int touristId, int tourInstanceId)
        {
            return _followingToursLive.Find(x => x.TourInstanceId == tourInstanceId && x.TouristsIds.Any(t => t == touristId));
        }
    }
}
