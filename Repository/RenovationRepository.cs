using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.View.Guest1;
using BookingApp.View.Owner;
using BookingApp.Domain.Model;

namespace BookingApp.Repository
{
    public class RenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovations.csv";

        private readonly AccommodationRepository _accommodationRepository;

        private readonly Serializer<Renovation> _serializer;

        private List<Renovation> _renovations;

        public List<Renovation> getRenovations()
        {
            return _renovations;
        }

        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = _serializer.FromCSV(FilePath);
            _accommodationRepository = new AccommodationRepository();
        }

        public List<Renovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Renovation Save(Renovation Renovation)
        {
            Renovation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(Renovation);
            _serializer.ToCSV(FilePath, _renovations);
            return Renovation;
        }

        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(a => a.Id) + 1;
        }

        public void Delete(Renovation Renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation founded = _renovations.Find(a => a.Id == Renovation.Id);
            _renovations.Remove(founded);
            _serializer.ToCSV(FilePath, _renovations);
        }

        public Renovation Update(Renovation Renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation current = _renovations.Find(a => a.Id == Renovation.Id);
            int index = _renovations.IndexOf(current);
            _renovations.Remove(current);
            _renovations.Insert(index, Renovation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _renovations);
            return Renovation;
        }

        public List<Renovation> GetAllUserReservations(int userId)
        {
            return _renovations.Where(r => r.UserId == userId).ToList();
        }

        public Renovation GetById(int id)
        {
            return _renovations.Where(r => r.Id == id).FirstOrDefault();
        }
        public List<Renovation> GetAllByUserId(int id)
        {
            return _renovations.Where(r => r.UserId == id).ToList();
        }
        public DateTime GetCheckInDate(int userId, int reservationId)
        {
            List<Renovation> renovations = GetAllUserReservations(userId);
            return renovations.Find(r => r.Id == reservationId).RenovationDateRange.StartDate;
        }
        public DateTime GetCheckOutDate(int userId, int reservationId)
        {
            List<Renovation> renovations = GetAllUserReservations(userId);
            return renovations.Find(r => r.Id == reservationId).RenovationDateRange.EndDate;
        }

        public Dictionary<int, string> GetReservationsByUserId(int userId)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<Renovation> usersReservations = GetAllUserReservations(userId).Where(r => r.RenovationDateRange.StartDate >= DateTime.UtcNow).ToList();
            if (usersReservations.Count > 0)
            {
                foreach (Renovation renovation in usersReservations)
                {

                    Renovation founded = GetById(renovation.Id);
                    string value = "";
                    string accommodationName = _accommodationRepository.getNameById(renovation.AccomodationId);
                    value = value + " " + accommodationName + "; " + founded.RenovationDateRange.SStartDate + "-" + founded.RenovationDateRange.SEndDate;
                    result.Add(renovation.Id, value);
                }
                return result;
            }
            return result;

        }

        public Accommodation GetAccommodationForRenovation(Renovation renovation)
        {
            return renovation.GetAccommodationById(_accommodationRepository.GetAll(), renovation.AccomodationId);
        }

    }
}
