using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        public int NextId();

        public Tour JustSave(Tour tour);

        public Tour Save(Tour tour);

        public Tour GetById(int id);
    }
}
