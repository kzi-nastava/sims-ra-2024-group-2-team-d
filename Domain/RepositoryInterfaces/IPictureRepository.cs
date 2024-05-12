using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IPictureRepository
    {
        public Picture Save(Picture picture);

        public int NextId();

        public List<Picture> GetAll();

        public List<string> GetByTourId(int tourId);
    }
}
