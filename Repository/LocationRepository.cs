using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System.Collections.Generic;

namespace BookingApp.Repository
{
    internal class LocationRepository : ILocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }
        public List<Location> getAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Location Save(Location locations)
        {
            _locations.Add(locations);
            _serializer.ToCSV(FilePath, _locations);
            return locations;
        }

        public void Delete(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location foundedLocation = _locations.Find(loc => loc.City == location.City && location.Country == location.Country);
            _locations.Remove(foundedLocation);
            _serializer.ToCSV(FilePath, _locations);
        }

        public Location GetById(int id)
        {
            return _locations.Find(l => l.Id == id);
        }
    }
}
