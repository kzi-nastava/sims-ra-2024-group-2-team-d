using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface ICommentService
    {
        List<Comment> GetAll();
        Comment Save(Comment comment);
        void Delete(Comment comment);
        Comment Update(Comment comment);
        List<Comment> GetByUser(int userId);
        Comment GetByCommentId(int commentId);
    }
}
