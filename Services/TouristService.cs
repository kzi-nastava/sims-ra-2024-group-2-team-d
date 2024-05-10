using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TouristService
    {
        public TouristRepository TouristRepository { get; set; }
        public TouristService() { 
            TouristRepository = new TouristRepository();
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
    }
}
