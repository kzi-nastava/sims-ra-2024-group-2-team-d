using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovationRepository
    {

        public List<Renovation> GetAll();

        public Renovation Save(Renovation Renovation);

        public int NextId();

        public void Delete(Renovation Renovation);

        public Renovation Update(Renovation Renovation);

        public List<Renovation> GetAllUserReservations(int userId);

        public Renovation GetById(int id);
        public List<Renovation> GetAllByUserId(int id);
        public DateTime GetCheckInDate(int userId, int reservationId);
        public DateTime GetCheckOutDate(int userId, int reservationId);

        public Dictionary<int, string> GetReservationsByUserId(int userId);


        public Accommodation GetAccommodationForRenovation(Renovation renovation);

    }
}
