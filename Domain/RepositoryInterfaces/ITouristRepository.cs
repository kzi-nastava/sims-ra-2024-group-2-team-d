using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristRepository
    {
        public Tourist Save(Tourist tourist);
        public Tourist Update(Tourist tourist);

        public List<Tourist> GetAllByTourReservationId(int id);

        public List<Tourist> GetByIds(List<int> touristIds);

        public Tourist? GetByUserAndReservationId(int userId, int reservationId);

        public List<Tourist> GetAll();

        public Tourist GetById(int id);
    }
}
