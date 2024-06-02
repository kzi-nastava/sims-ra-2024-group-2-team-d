using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class KeyPointService : IKeyPointService
    {
        private IKeyPointRepository KeyPointRepository { get; set; }

        public KeyPointService()
        {
            KeyPointRepository = Injector.Injector.CreateInstance<IKeyPointRepository>();
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
