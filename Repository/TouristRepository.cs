using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TouristRepository
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


        public List<Tourist> GetAllByTourReservationId(int id)
        {
            return _tourists.Where(t => t.ReservationId == id).ToList();

        }

    }
}
