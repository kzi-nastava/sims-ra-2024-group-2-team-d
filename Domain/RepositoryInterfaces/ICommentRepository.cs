using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        public List<Comment> GetAll();
        public Comment Save(Comment comment);

        public int NextId();

        public void Delete(Comment comment);

        public Comment Update(Comment comment);

        public List<Comment> GetByUser(int userID);


    }
}
