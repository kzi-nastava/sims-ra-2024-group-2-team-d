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
    }
}
