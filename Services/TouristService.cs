using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Services
{
    public class TouristService
    {
        public ITouristRepository TouristRepository { get; set; }
        public TouristService(ITouristRepository touristRepository) { 
            TouristRepository = touristRepository;
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
