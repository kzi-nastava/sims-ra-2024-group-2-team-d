using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TouristRepository : ITouristRepository
    {
        private const string FilePath = "../../../Resources/Data/tourists.csv";

        private readonly Serializer<Tourist> _serializer;

        private List<Tourist> _tourists;

        public TouristRepository()
        {
            _serializer = new Serializer<Tourist>();
            _tourists = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _tourists = _serializer.FromCSV(FilePath);
            if (_tourists.Count < 1)
            {
                return 1;
            }
            return _tourists.Max(c => c.Id) + 1;
        }

        public Tourist Save(Tourist tourist)
        {
            tourist.Id = NextId();
            _tourists = _serializer.FromCSV(FilePath);
            _tourists.Add(tourist);
            _serializer.ToCSV(FilePath, _tourists);
            return tourist;
        }
        public Tourist Update(Tourist tourist)
        {
            _tourists = _serializer.FromCSV(FilePath);
            Tourist current = _tourists.Find(c => c.Id == tourist.Id);
            int index = _tourists.IndexOf(current);
            _tourists.Remove(current);
            _tourists.Insert(index, tourist);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourists);
            return tourist;
        }

        public List<Tourist> GetAllByTourReservationId(int id)
        {
            return _tourists.Where(t => t.ReservationId == id).ToList();
        }

        public List<Tourist> GetByIds(List<int> touristIds)
        {
            return _tourists.Where(t => touristIds.Contains(t.Id)).ToList();
        }

        public Tourist? GetByUserAndReservationId(int userId, int reservationId)
        {
            return _tourists.Find(x => x.UserId == userId && x.ReservationId == reservationId);
        }

        public List<Tourist> GetAll()
        {
            return _tourists;
        }

        public Tourist GetById(int id)
        {
            return _tourists.Find(x => x.UserId == id);
        }
    }
}
