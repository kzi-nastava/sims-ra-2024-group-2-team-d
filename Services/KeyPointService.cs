using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class KeyPointService
    {
        public KeyPointRepository KeyPointRepository { get; set; }

        public KeyPointService() 
        { 
            KeyPointRepository = new KeyPointRepository();
        }

        public KeyPoint Save(KeyPoint keyPoint)
        {
            return KeyPointRepository.Save(keyPoint);
        }

        public List<KeyPoint> GetByTourInstance(TourInstance tourInstance)
        {
            return KeyPointRepository.GetAll().FindAll(c => c.TourId == tourInstance.TourId);
        }

        public string GetKeyPointName(int keyPointId)
        {
            return KeyPointRepository.GetKeyPointName(keyPointId);
        }

        public KeyPoint GetById(int Id)
        {
            return KeyPointRepository.GetById(Id);
        }

    }
}
