using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IKeyPointService
    {
        public KeyPoint Save(KeyPoint keyPoint);

        public List<KeyPoint> GetByTourInstance(TourInstance tourInstance);

        public string GetKeyPointName(int keyPointId);

        public KeyPoint GetById(int Id);
    }
}
