using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumRepository
    {

        List<Forum> GetAll();
        Forum Save(Forum forum);
        void Update(Forum forum);

    }
}

