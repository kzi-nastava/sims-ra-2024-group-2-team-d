using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services
{
    public class ForumCommentService : IForumCommentService
    {
        private readonly IForumCommentRepository _forumCommentRepository;
        public ForumCommentService()
        {
            _forumCommentRepository = Injector.Injector.CreateInstance<IForumCommentRepository>();
        }
        public void Delete(ForumComment forum)
        {
            _forumCommentRepository.Delete(forum);
        }

        public List<ForumComment> GetAll()
        {
            return _forumCommentRepository.GetAll();
        }

        public List<int> GetCommentsIdByForumId(int forumId)
        {
            List<ForumComment> forums = _forumCommentRepository.GetAll();
            List<ForumComment> forumComments = new List<ForumComment>();
            List<int> result = new List<int>();
            forumComments = forums.Where(forum => forum.ForumId == forumId).ToList();
            foreach (ForumComment forum in forumComments)
            {
                result.Add(forum.CommentId);
            }
            return result;
        }

        public int GetCommentsNumberByForum(int forumId)
        {
            List<ForumComment> forums = _forumCommentRepository.GetAll();
            return forums.Where(forum => forum.ForumId == forumId).Count();
        }

        public ForumComment Save(ForumComment forum)
        {
            return _forumCommentRepository.Save(forum);
        }

        public void Update(ForumComment forum)
        {
            _forumCommentRepository.Update(forum);
        }
    }
}
