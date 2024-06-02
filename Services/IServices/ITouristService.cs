using BookingApp.Domain.Model;
using BookingApp.Repository;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface ITouristService
    {
        public Tourist Update(Tourist tourist);

        public Tourist Save(Tourist tourist);

        public List<Tourist> GetByIds(List<int> ids);

        public Tourist GetById(int id);

        public List<Tourist> GetAll();
    }
}
