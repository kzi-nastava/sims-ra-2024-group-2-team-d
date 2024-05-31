using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IUserService
    {
        User GetById(int id);
        List<User> GetAll();

        bool CheckSuperGuide(User user, List<TourInstance> TourInstances);

        List<User> GetAllSuperGuides(List<TourInstance> TourInstances);
    }
}
