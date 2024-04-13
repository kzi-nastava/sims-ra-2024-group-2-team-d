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
    }
}
