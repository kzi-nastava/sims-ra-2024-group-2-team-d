using BookingApp.Domain.Model;
using BookingApp.Repository;
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

        public Tourist Save(Tourist tourist)
        {
           return TouristRepository.Save(tourist);
        }

        public List<Tourist> GetByIds(List<int> ids)
        {
            return TouristRepository.GetByIds(ids);
        }
    }
}
