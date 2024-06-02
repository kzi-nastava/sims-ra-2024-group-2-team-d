using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class ForumRepository : IForumRepository
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";
        private readonly Serializer<Forum> _serializer;
        private List<Forum> _forums;

        public ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _forums = new List<Forum>();
        }

        public List<Forum> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Forum Save(Forum forum)
        {
            forum.ForumId = NextId();
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }

        private int NextId()
        {
            _forums = GetAll();
            if (_forums.Count < 1)
            {
                return 1;
            }
            return _forums.Max(f => f.ForumId) + 1;
        }


        public void Update(Forum forum)
        {
            _forums = GetAll();
            Forum existingForum = _forums.Find(f => f.ForumId == forum.ForumId);
            if (existingForum != null)
            {
                existingForum.ForumTopic = forum.ForumTopic;
                existingForum.Location = forum.Location;
                existingForum.DateCreated = forum.DateCreated;
                existingForum.Comments = forum.Comments;
                existingForum.Status = forum.Status;
                existingForum.VeryUseful = forum.VeryUseful;
                _serializer.ToCSV(FilePath, _forums);
            }
        }
    }
}
