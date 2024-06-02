using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        public List<Location> getAll();
        public Location Save(Location locations);
        public void Delete(Location location);
        public Location GetById(int id);
    }
}
