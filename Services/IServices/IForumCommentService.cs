using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IForumCommentService
    {
        List<ForumComment> GetAll();
        ForumComment Save(ForumComment forum);
        void Delete(ForumComment forum);
        void Update(ForumComment forum);
        int GetCommentsNumberByForum(int forumId);
        List<int> GetCommentsIdByForumId(int forumId);
    }
}
