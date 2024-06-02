using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService()
        {
            _commentRepository = Injector.Injector.CreateInstance<ICommentRepository>();
        }
        public void Delete(Comment comment)
        {
            _commentRepository.Delete(comment);
        }

        public List<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public Comment GetByCommentId(int commentId)
        {
            return GetAll().Find(comment => comment.Id == commentId);
        }

        public List<Comment> GetByUser(int userId)
        {
            return _commentRepository.GetByUser(userId);
        }
        public Comment Save(Comment comment)
        {
            return _commentRepository.Save(comment);
        }

        public Comment Update(Comment comment)
        {
            return _commentRepository.Update(comment);
        }
    }
}
