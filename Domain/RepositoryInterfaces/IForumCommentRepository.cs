using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumCommentRepository
    {
        List<ForumComment> GetAll();
        ForumComment Save(ForumComment forum);
        void Delete(ForumComment forum);
        void Update(ForumComment forum);
    }
}
