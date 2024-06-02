using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface ILocationService
    {
        public List<Location> GetAll();
        public Location Save(Location locations);
        public void Delete(Location location);
        public Location GetById(int id);
        public List<string> GetCitiesByCountry(string country);
        public List<string> GetAllCountries();
        public List<string> GetAllCities();
    }
}
