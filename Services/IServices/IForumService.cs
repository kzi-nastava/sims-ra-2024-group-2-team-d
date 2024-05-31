using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IForumService
    {
        List<Forum> GetAll();
        Forum Save(Forum forum);
        void Update(Forum forum);
        List<Forum> GetForumsByCreatorId(int userId);
        List<Forum> GetActiveForumsByCreatorId(int userId);
        Forum GetForumById(int forumId);
        Dictionary<int, string> GetForumsByUserKeyValue(int userId);
        string GetTopic(int forumId);
        Location GetLocation(int forumId);
    }
}
