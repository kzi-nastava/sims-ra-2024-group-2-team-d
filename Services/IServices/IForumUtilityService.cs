using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IForumUtilityService
    {
        string CheckUseful(Forum forum);
        public List<Forum> GetForumsWhereOwnerHasAccommodation();
    }
}
