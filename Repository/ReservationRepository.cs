using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class ReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/Reservations.csv";

        private readonly Serializer<Reservation> _serializer;

        private List<Reservation> _Reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _Reservations = _serializer.FromCSV(FilePath);
        }

        public List<Reservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation Reservation)
        {
            Reservation.Id = NextId();
            _Reservations = _serializer.FromCSV(FilePath);
            _Reservations.Add(Reservation);
            _serializer.ToCSV(FilePath, _Reservations);
            return Reservation;
        }

        public int NextId()
        {
            _Reservations = _serializer.FromCSV(FilePath);
            if (_Reservations.Count < 1)
            {
                return 1;
            }
            return _Reservations.Max(a => a.Id) + 1;
        }

        public void Delete(Reservation Reservation)
        {
            _Reservations = _serializer.FromCSV(FilePath);
            Reservation founded = _Reservations.Find(a => a.Id == Reservation.Id);
            _Reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _Reservations);
        }

        public Reservation Update(Reservation Reservation)
        {
            _Reservations = _serializer.FromCSV(FilePath);
            Reservation current = _Reservations.Find(a => a.Id == Reservation.Id);
            int index = _Reservations.IndexOf(current);
            _Reservations.Remove(current);
            _Reservations.Insert(index, Reservation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _Reservations);
            return Reservation;
        }

        public List<Reservation> GetAllUnreviewed(List<int> accomodationId)
        {
            List<Reservation> list = new List<Reservation> ();
            foreach (var id in accomodationId)
            {
                foreach (var review in GetAll().Where(r => r.AccomodationId == id).Where(r => r.ReservationDateRange.EndDate <= System.DateTime.Today && r.ReservationDateRange.EndDate.AddDays(5) >= System.DateTime.Today).Where(r => r.ReviewedByOwner == false).ToList())
                {
                    list.Add(review);
                }

            }
            return list;
        }
    }
}
