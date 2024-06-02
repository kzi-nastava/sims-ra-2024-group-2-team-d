using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class TouristService : ITouristService
    {
        public ITouristRepository TouristRepository { get; set; }
        public TouristService()
        {
            TouristRepository = Injector.Injector.CreateInstance<ITouristRepository>();
        }

        public Tourist Update(Tourist tourist)
        {
            return TouristRepository.Update(tourist);
        }

        public Tourist Save(Tourist tourist)
        {
            return TouristRepository.Save(tourist);
        }

        public List<Tourist> GetByIds(List<int> ids)
        {
            return TouristRepository.GetByIds(ids);
        }

        public Tourist GetById(int id)
        {
            return TouristRepository.GetById(id);
        }

        public List<Tourist> GetAll()
        {
            return TouristRepository.GetAll();
        }
    }
}
