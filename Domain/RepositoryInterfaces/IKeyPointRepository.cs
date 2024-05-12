using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IKeyPointRepository
    {
        public List<KeyPoint> GetAll();

        public int NextId();

        public KeyPoint Save(KeyPoint keyPoint);

        public KeyPoint GetById(int Id);

        public void Delete(KeyPoint keyPoints);

        public KeyPoint Update(KeyPoint keyPoints);

        public List<KeyPoint> GetByTourId(int tourId);

        public List<KeyPoint> GetByTourInstance(TourInstance tourInstance);

        public string GetKeyPointName(int keyPointId);
    }
}
